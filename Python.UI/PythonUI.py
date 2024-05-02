from ast import Global
from asyncio.windows_events import NULL
import signal
import tkinter as tk
from turtle import circle, color
from urllib import response
import json
import requests
from functools import partial
from enum import Enum
from tkinter import INSERT, Canvas, messagebox
from signalrcore.hub_connection_builder import HubConnectionBuilder


class SignalR:

    hubaddress = "https://localhost:7081/TroubleHub"
    #hubaddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";

    hub_connection = HubConnectionBuilder().with_url(hubaddress, options={"verify_ssl": False}).build()
    hub_connection.start()
    
    hub_connection.on("ReceiveMessage", lambda msg: print("Received message back from hub." + msg[1]))
    
    def send_message(self, message):
        self.hub_connection.send("SendMessage", [message])
    
signalr = SignalR()
circle_id = 1 

class LoginScreen:
    def __init__(self, master):
        self.master = master
        master.title("Login")
        self.frame = tk.Frame(master)
        self.frame.pack()

        # Creating widgets for login screen
        self.login_label = tk.Label(self.frame, text="Login", font=("Arial", 20))
        self.username_label = tk.Label(self.frame, text="Username", font=("Arial", 16))
        self.username_entry = tk.Entry(self.frame)
        self.password_label = tk.Label(self.frame, text="Password", font=("Arial", 16))
        self.password_entry = tk.Entry(self.frame, show="*")
        self.login_button = tk.Button(self.frame, text="Login", command=self.login)

        # Grid layout for login screen widgets
        self.login_label.grid(row=0, column=0, columnspan=2, pady=40)
        self.username_label.grid(row=1, column=0)
        self.username_entry.grid(row=1, column=1, pady=20)
        self.password_label.grid(row=2, column=0)
        self.password_entry.grid(row=2, column=1, pady=20)
        self.login_button.grid(row=3, column=0, columnspan=2, pady=30)        

    # def login(self):
    #     #retrieve username and password by user
    #     username = self.username_entry.get()
    #     password = self.username_entry.get()
        
    #     if username and password:
    #         try: 
    #             hub_connection.send("Login", [username, password])
    #             messagebox.showinfo(title="Login Success", message="You successfully logged in")
    #             self.master.withdraw()  # Hide login window
            
    #             options_window = tk.Toplevel(self.master)
    #             options_screen = OptionsScreen(options_window)
                
    #         except Exception as e:
    #              messagebox.showerror(title="Error", message=str(e))
                      
    #     else:
    #         messagebox.showinfo(title="Error", message="Invalid Login")
    
    def login(self):
        userworks = "User1"
        passwordworks = "Test"
        if self.username_entry.get() == userworks and self.password_entry.get() == passwordworks:
            signalr.hub_connection.send("Login", [userworks, passwordworks])
            messagebox.showinfo(title="Login Success", message="You successfully logged in")
            self.master.withdraw()  # Hide login window
            
            options_window = tk.Toplevel(self.master)
            options_screen = OptionsScreen(options_window)
                      
        else:
            messagebox.showinfo(title="Error", message="Invalid Login")
            

class OptionsScreen():
    def __init__(self, master):
        self.master = master
        master.title("Choose")

        self.frame = tk.Frame(master)
        self.frame.pack()

        # Creating widgets for Option screen
        self.option_label = tk.Label(self.frame, text="Choose an Option", font=("Arial", 20))
        self.game_button = tk.Button(self.frame, text="Choose Game", command=lambda: StartGame(master))
        self.logout_button = tk.Button(self.frame, text="Log Out", command=lambda: LogOut(master))

        #Grid Layout
        self.option_label.grid(row=0, column=0, columnspan=2, pady=40)
        self.game_button.grid(row=1, column=0)
        self.logout_button.grid(row=1, column=1, pady=20)   
       

#create class for selecting game        


#hit database
def LogOut(master):
    user = "User1"
    signalr.hub_connection.send("Logout",[user])
    master.withdraw()
    
    login_window = tk.Toplevel(master)
    login_screen = LoginScreen(login_window)
    

#-----------------------------------------Game Aspect-------------------------------------
class Color(Enum):
     Green = 0
     Yellow = 1
     Blue = 2
     Red = 3

def StartGame(master):
    master.withdraw()
    game_window = tk.Toplevel(master)
    game_screen = TroubleBoard(game_window)

 
def TupleFinder(zones):
        coordinates = []
        for zone_type, zone_list in zones.items():
            for coordinate in zone_list:
                coordinates.append(coordinate)
        return coordinates

def print_circles(canvas):
    circle_info = []
    circles = canvas.find_withtag("circle")
    for circle_id in circles:
        tags = canvas.gettags(circle_id)
        color = None
        piece_id = None
        for tag in tags:
            if tag.startswith("color_"):
                color = tag.split("_")[1]
            elif tag.startswith("id_"):
                piece_id = tag.split("_")[1]
        if color and piece_id:
            circle_info.append((circle_id, color, piece_id))
        else:
            circle_info.append((circle_id, None, None))
    return circle_info

class TroubleBoard:
    
    def assign_pieces_to_circles(self, circle_ids):
        #response = requests.get("https://bigprojectapi-300077578.azurewebsites.net/api/PieceGame/")
        response = requests.get("https://localhost:7081/api/PieceGame/", verify=False)
        data = response.json()
        
            # Sort the data by piece color (Guids)
        data.sort(key=lambda x: x['pieceColor'])
        
        # Sort the circle IDs based on piece colors
        circle_ids.sort(key=lambda circle_id: self.canvas.itemcget(circle_id, "fill"))
       
        for item, circle_id in zip(data, circle_ids):
            piece_id = item['pieceId']
            Color = item['pieceColor']
            gameId = item['gameId']
            Location = item['pieceLocation']

            # Check if the circle_id corresponds to a circle
            if "circle" in self.canvas.gettags(circle_id):
                # Update the color of the circle on the canvas
                self.canvas.itemconfig(circle_id, fill=Color)
                
                # Add the piece ID to the circle tags
                self.canvas.itemconfig(circle_id, tags=(self.canvas.gettags(circle_id) + (f"id_{piece_id}",)))
                
            # Move the circle to the correct location
            if Location != 0:
                # Get the coordinates of the corresponding spot
                coordinate1 = list(self.coordinate_mapping)
                pieceLocation = coordinate1[Location - 1]
                
                # Calculate the center coordinates of the circle
                center_x = pieceLocation[0] * self.square_size + self.square_size // 2
                center_y = pieceLocation[1] * self.square_size + self.square_size // 2
                
                # Move the circle to the correct location
                self.canvas.coords(circle_id, center_x - self.square_size // 2, center_y - self.square_size // 2,
                                    center_x + self.square_size // 2, center_y + self.square_size // 2)
                

              
                

                #spot_id = self.coordinate_mapping[Location]
                #print(spot_id)
                
                #circle ID
                #self.canvas.itemconfig(circle_id, tags=(self.canvas.gettags(circle_id) + piece_id))

                # Store the piece ID in the circle mapping
                #self.coordinate_mapping[circle_id] = piece_id
                
               
                
                #Add the piece ID and spot ID to the circle tags
                #spot_id = self.coordinate_mapping[Location]
                #self.canvas.itemconfig(circle_id, tags=(self.canvas.gettags(circle_id) + (f"id_{spot_id}",)))
                
                
        
                #prints out the value 1
                #coordinate = list(self.coordinate_mapping.values())
                
                #print(coordinate[Location - 1])


                #print(f"Circle ID: {circle_id}, Piece ID: {piece_id}")
                #print(f"Circle ID: {circle_id}, Piece ID: {piece_id}, Spot ID: {spot_id}")
            # else:
            #     print(f"Item with ID {circle_id} is not a circle.")
            
    def on_button_click(self):
        user = "User1"
        #signalr.hub_connection.send("RollDice", [user])
        signalr.hub_connection.on("DiceRolled", lambda msg: self.text_dice_roll(msg))
        signalr.hub_connection.send("RollDice", [user])
        #signalr.hub_connection.send("RollDice", )
        #self.text_dice_roll()
       
        
    def text_dice_roll(self, msg):
        
         # Convert the integer to a string
        result_str = str(msg[0])
        # Update the label text with the rolled dice result
        self.dice_result_label.config(text="You rolled a: " + result_str, font=("Arial", 16, "bold"))
        
        self.rolled_number = msg[0]

   
    
    def button(self):
        button = tk.Button(self.master, text="Roll!", command=self.on_button_click)
        button.grid(row=0, column=0, padx=0, pady=0)   

    def __init__(self, master):
        self.master = master
        self.master.title("Trouble Game Board")
        self.board_size = 19
        self.square_size = 35  # Adjust for desired square size
        self.canvas = tk.Canvas(master, width=self.board_size * self.square_size, height=self.board_size * self.square_size, bg="white")
        self.canvas.grid()
        self.canvas.bind("<Button-1>", self.on_piece_click)

        self.dice_result_label = tk.Label(master, text="Dice Roll Result: ", font=("Arial", 16, "bold"))
        self.dice_result_label.grid(row=1, column=0)
        
        self.button()
       

        #need this to store the ID of the tuple in a dictionary 
        #this is for the spots around 
        self.coordinate_mapping = {}  #Dictionary to store mapping of tuples to integers   
        self.current_id = 1  # Start the ID from 1


        
        for i in range(self.board_size):
            for j in range(self.board_size):
                x1 = i * self.square_size
                y1 = j * self.square_size
                x2 = x1 + self.square_size
                y2 = y1 + self.square_size
                self.canvas.create_rectangle(x1, y1, x2, y2, outline="black", fill="white")
                
        
        #might need to modify this as well in order to add the colors for the specific spaces
        self.colors = {
            'draw_green_zone_left': "green", 
            'draw_red_zone_left': "red",
            'draw_red_zone_right': "red",
            'draw_blue_zone_left': "blue",
            'draw_blue_zone_right': "blue",
            'draw_yellow_zone_right': "yellow",
            'draw_yellow_zone_left': "yellow",
            'draw_green_zone_right': "green",

            'draw_green_home_zones': "green",
            'draw_red_home_zones': "red",
            'draw_blue_home_zones': "blue",
            'draw_yellow_home_zones': "yellow",
            
            'draw_green_starting_zones' : "green",
            'draw_red_starting_zones' : "red",
            'draw_blue_starting_zones' : "blue",
            'draw_yellow_starting_zones' : "yellow",
        }
        
        self.draw_board()
       
        
    def TuplePieceMover(self, circle_id):
        tags = self.canvas.gettags(circle_id)
        piece_id = None
        
        for tag in tags:
            if "-" in tag:
                piece_id = tag.split("_")[1]
            
        if piece_id is None:
            print ("Piece ID was not found")
            return

        game_id = "3d02117a-4051-460a-ba4d-baf5d4e583be"
            
        print(f"Clicked on circle with ID: {circle_id}, Piece ID: {piece_id}")
        
        
        
            
        signalr.hub_connection.send("MovePiece", [piece_id, game_id, self.rolled_number])
            
            
    def on_piece_click(self, event):
            clicked_object = self.canvas.find_closest(event.x, event.y)[0]
            if "circle" in self.canvas.gettags(clicked_object):
                 self.TuplePieceMover(clicked_object)
            else:
                 print("Clicked on empty space") 
                 # Get the ID of the clicked object
                 clicked_object = self.canvas.find_closest(event.x, event.y)[0]
                 # Check if the clicked object is a circle
                 if "circle" in self.canvas.gettags(clicked_object):
                     # Call TupleMover method with the ID of the clicked circle
                     self.TupleMover(clicked_object)
                 # else:
                 #     print("Clicked on empty space")

             
    def draw_board(self):
        
        
        game_zones = {
            
            'draw_green_zone_left': [(3, 10), (3, 9), (4, 8), (5, 7)],

            'draw_yellow_zone_left' : [(6, 6), (7, 5), (8, 4)],

            'draw_yellow_zone_right': [(9, 4), (10, 4), (11, 5), (12, 6)],
            
             'draw_blue_zone_right': [(13, 7), (14, 8), (15, 9)],
             
             'draw_blue_zone_left': [(15, 10), (15, 11), (14, 12), (13, 13)],
             
             'draw_red_zone_right': [(12, 14), (11, 15), (10, 16)],
             
             'draw_red_zone_left': [(9, 16), (8, 16), (7, 15), (6, 14)], 
             
             'draw_green_zone_right': [(5, 13), (4, 12), (3, 11)], 
                                
        }
        starting_zones = {
            'draw_green_starting_zones' : [(1,8), (1,9), (1,11), (1,12)],
            
            'draw_red_starting_zones' : [(7, 18), (8, 18), (10, 18), (11, 18)],
            
            'draw_blue_starting_zones' : [(17, 8), (17, 9), (17, 11), (17, 12)],
            
            'draw_yellow_starting_zones' : [(7, 2), (8, 2), (10, 2), (11, 2)],
            
        }
        home_zones = {
            'draw_green_home_zones': [(7, 10), (6, 10), (5, 10), (4, 10)],
            
            'draw_red_home_zones' : [(9, 12), (9, 13), (9, 14), (9, 15)],
            'draw_blue_home_zones' : [(11, 10), (12, 10), (13, 10), (14, 10)],
            
            'draw_yellow_home_zones' : [(9, 8), (9, 7), (9, 6), (9, 5)]
        }
        
        
     
        # Call TupleFinder to get the coordinates for each zone
        # game_zone_coordinates = TupleFinder(game_zones)
        # starting_zone_coordinates = TupleFinder(starting_zones)
        # home_zone_coordinates = TupleFinder(home_zones)
        
        # print("Game Zone Coordinates:", game_zone_coordinates)
        # print("Starting Zone Coordinates:", starting_zone_coordinates)
        # print("Home Zone Coordinates:", home_zone_coordinates)
        
       #spots on the game board
        for zone_type, zone_list in game_zones.items():
            for game_zone in zone_list:
                #this code adds the ID to the specific tuple
                x, y = game_zone
                self.coordinate_mapping[(x, y)] = self.current_id
                self.current_id += 1
                spot_id = self.coordinate_mapping[(x, y)]
                
                print(f"Spot at ({x}, {y}) has ID: {spot_id}")
                
                

                #just draws it
                center_x = x * self.square_size + self.square_size // 2
                center_y = y * self.square_size + self.square_size // 2
                square_size = self.square_size // 2
                self.canvas.create_rectangle(center_x - square_size, center_y - square_size, center_x + square_size,
                                              center_y + square_size, fill=self.colors[zone_type], outline="black", tags=("spot_id"))   
                
                self.canvas.create_text(center_x, center_y, text=str(spot_id), font=("Arial", 10, "bold"))
        # print(f"Coordinates: {self.coordinate_mapping}")
        

        # #prints out the tuple (3,10)
        # coordinate1 = list(self.coordinate_mapping)
        # print(coordinate1[0])
        
        # #prints out the value 1
        # coordinate = list(self.coordinate_mapping.values())
        # print(coordinate[0])
        

        #---------------------------------------------------------------------------------------------------------------------------------------------------------
        #Initialize an empty list to store circle IDs
        circle_ids = []

        #Start the circle ID from 1
        circle_id = 1  

        #Loop through the starting zones to create circles and assign piece IDs
        for zone_type, zone_list in starting_zones.items():
            for i, starting_zone in enumerate(zone_list, start=1):
                x, y = starting_zone

                # Add piece ID to the coordinate mapping
                self.coordinate_mapping[(x, y)] = circle_id
                piece_id = circle_id  # Assign piece ID before accessing it

                # Draw circular piece
                center_x = x * self.square_size + self.square_size // 2
                center_y = y * self.square_size + self.square_size // 2
                radius = self.square_size // 2

                # Create a circle and tag it as "circle" to identify it later
                circle = self.canvas.create_oval(center_x - radius, center_y - radius, center_x + radius,
                        center_y + radius, fill=self.colors[zone_type], outline="Orange", tags=("circle", f"color_{self.colors[zone_type]}"))

                # Add additional tags for color and piece ID
                self.canvas.itemconfig(circle, tags=("circle", f"color_{zone_type}", f"id_{piece_id}"))

                # Append the circle ID to the circle_ids list
                circle_ids.append(circle)  # Append the circle object, not the circle_id

                #text = f"{i}"  # Example: "1"
                #self.canvas.create_text(center_x, center_y, text=text, font=("Arial", 5, "bold"))

            # Increment the circle ID by the total number of circles created in the zone
            circle_id += len(zone_list)

        # Print the circle IDs for verification
        #print("Circle IDs:", circle_ids)

        # Call the assign_pieces_to_circles method with the generated circle IDs
        self.assign_pieces_to_circles(circle_ids)

                

        #HOME ZONES TO WIN THE GAME
        for zone_type, zone_list in home_zones.items():
                for i, starting_zone in enumerate(zone_list, start=1):
                    x, y = starting_zone
                    # Just draws it
                    center_x = x * self.square_size + self.square_size // 2
                    center_y = y * self.square_size + self.square_size // 2
                    square_size = self.square_size // 2
                    self.canvas.create_rectangle(center_x - square_size, center_y - square_size, center_x + square_size,
                                                  center_y + square_size, fill=self.colors[zone_type], outline="black")    
                    self.canvas.create_text(center_x, center_y, text=str(i), font=("Arial", 10, "bold"))
                    
                    

def main():    
    root = tk.Tk()
    login_screen = LoginScreen(root)
    root.mainloop()

if __name__ == "__main__":
    main()


