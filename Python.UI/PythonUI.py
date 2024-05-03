from ast import Global, Str
from asyncio.windows_events import NULL
from lib2to3.pgen2.pgen import DFAState
from pickle import TRUE
from re import X
import signal
import tkinter as tk
import tkinter
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
#circle_id = 1 

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

    def login(self):
        #retrieve username and password by user
        username = self.username_entry.get()
        password = self.password_entry.get()

        if username and password:
            signalr.hub_connection.send("Login", [username, password])
            signalr.hub_connection.on("LoginResult", lambda msg: self.LoginResult(str(msg[0])))
  
    def LoginResult(self, msg):
        if(msg == "True"):
            messagebox.showinfo(title="Login Success", message="You successfully logged in")
            self.master.withdraw()  # Hide login window
            options_window = tk.Toplevel(self.master)
            options_screen = OptionsScreen(options_window)
        elif(msg == False):
            messagebox.showinfo(title="Error", message="Invalid Login")
         
class OptionsScreen():
    def __init__(self, master):
        self.master = master
        master.title("Choose")

        self.frame = tk.Frame(master)
        self.frame.pack()

        # Creating widgets for Option screen
        self.option_label = tk.Label(self.frame, text="Choose an Option", font=("Arial", 20))
        
        
        self.game_button = tk.Button(self.frame, text="Choose Game", command=lambda: ChooseGame(master))
        

        self.logout_button = tk.Button(self.frame, text="Log Out", command=lambda: LogOut(master))

        #Grid Layout
        self.option_label.grid(row=0, column=0, columnspan=2, pady=40)
        self.game_button.grid(row=1, column=0)
        self.logout_button.grid(row=1, column=1, pady=20) 
        
        def choose_game(self):
            # Hide the current options window
            self.master.withdraw()
        
            # Open the ChooseGame window
            choose_game_window = tk.Toplevel(self.master)
            choose_game_screen = ChooseGame(choose_game_window)
            

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
     
class ChooseGame:
    def __init__(self, master):
        self.master = master
        master.title("Choose Game")
        
        self.frame = tk.Frame(master)
        self.frame.pack()

        # Fetch game data from the API
        #response = requests.get("https://bigprojectapi-300077578.azurewebsites.net/api/Game/")
        response = requests.get("https://localhost:7081/api/Game/", verify=False)
        data = response.json()
        
        # Store game data
        self.game_data = data

        # Extract game names from the JSON data
        self.game_names = [item['gameName'] for item in data]
        self.turn_num = [item['turnNum'] for item in data]
        self.game_ids = [item['id'] for item in data]

        # Add game names to the listbox
        self.game_label = tk.Label(self.frame, text="Select a Game", font=("Arial", 20))
        self.game_listbox = tk.Listbox(self.frame, selectmode=tk.SINGLE)
        for game_name in self.game_names:
            self.game_listbox.insert(tk.END, game_name)

        self.select_button = tk.Button(self.frame, text="Select", command=self.select_game)
        self.game_label.pack()
        self.game_listbox.pack()
        self.select_button.pack()

        # Instance variable to store selected game ID
        self.selected_game_id = None

    def select_game(self):
        selected_index = self.game_listbox.curselection()
        if selected_index:
            selected_game_index = selected_index[0]
            selected_game_name = self.game_names[selected_game_index]
            self.selected_game_id = self.game_ids[selected_game_index]
            self.selected_turn_num = self.turn_num[selected_game_index]
            for game in self.game_data:
                if game['gameName'] == selected_game_name:
                    #messagebox.showinfo("Selected Game", f"Game ID: {self.selected_game_id}\nGame Name: {game['gameName']}\nTurn Number: {game['turnNum']}\nGame Date: {game['gameDate']}")
                    # Start the game based on the selected game
                    StartGame(self.master,  self.selected_game_id, self.selected_turn_num)
                    break
        else:
            messagebox.showinfo("Error", "Please select a game.")     
    

def StartGame(master, selected_game_id, selected_turn_num):
    master.withdraw()
    game_window = tk.Toplevel(master)
    game_screen = TroubleBoard(game_window, selected_game_id, selected_turn_num)

 
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
        #response = requests.get("https://bigprojectapi-300077578.azurewebsites.net/api/PieceGame/" + self.selected_game_id)
        response = requests.get("https://localhost:7081/api/PieceGame/" + self.selected_game_id, verify=False)
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
                self.canvas.itemconfig(circle_id, tags=(self.canvas.gettags(circle_id) + (f"Location_{Location}",)))
                
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
                       
    def on_button_click(self):
        
        #fix this 
        user = "User1"
        #signalr.hub_connection.send("RollDice", [user])
        signalr.hub_connection.on("DiceRolled", lambda msg: self.text_dice_roll(msg))
        #lambda msg: print("Received message back from hub."
       
        signalr.hub_connection.on("MovePieceReturn", lambda msg: self.move_piece_return(msg[0], msg[1]))

        # signalr.hub_connection.on("MovePieceReturn", lambda piece_Id, location: print(piece_Id + " " + location))
        signalr.hub_connection.send("RollDice", [user])
        #signalr.hub_connection.send("RollDice", )
        #self.text_dice_roll()
        
    def text_dice_roll(self, msg):
        
         # Convert the integer to a string
        result_str = str(msg[0])
        
        #Update the label text
        color_turn = self.selected_turn_num.name
        # Update the label text with the rolled dice result
        self.dice_result_label.config(text=f"Color Turn: {color_turn}, Rolled a: {result_str}", font=("Arial", 16, "bold"))
        #self.dice_result_label.config(text="You rolled a: " + result_str, font=("Arial", 16, "bold"))
        
        self.rolled_number = msg[0]
        
        #Enable piece movement after the dice is rolled 
        self.piece_movement_enable = True
        
    def move_piece_return(self, piece_Id, newLocation):
        #piece_Moved = False
        
        # print("Move_Piece_Return Function Test")
        print(piece_Id)
        print(newLocation)
        for item in self.canvas.find_withtag("circle"):
            tags = self.canvas.gettags(item)
            if f"id_{piece_Id}" in tags:
                #Found the piece with the specfic ID
                piece = item
                #piece location from its tags
                current_location = None
                print(f"got Piece")
                for tag in tags:
                    if tag.startswith("Location_"):
                        current_location = int(tag.split("_")[1])
                        print(f"Got Location spot_id{current_location}")
                        break
                    #if()
                print(f"Got Location spot_id{newLocation}")
                if current_location is not None:
                    #if the piece is moving to a new location 
                    if newLocation != 0:
                        if current_location != newLocation:
                            if current_location <= 28:
                                piece_Moved = True
                                 # Update the piece's location
                                self.canvas.itemconfig(piece, tags=tuple(tag for tag in tags if not tag.startswith("Location_")) + (f"Location_{newLocation}",))
                                #self.canvas.itemconfig(piece,tags=(tags - (f"Location_{current_location}",) + (f"Location_{newLocation}",)))
                            
                                # Get the coordinates of the corresponding spot
                                coordinate1 = list(self.coordinate_mapping)
                                pieceLocation = coordinate1[newLocation - 1 ]
                            
                                # Calculate the center coordinates of the circle
                                center_x = pieceLocation[0] * self.square_size + self.square_size // 2
                                center_y = pieceLocation[1] * self.square_size + self.square_size // 2

                                self.canvas.coords(item, center_x - self.square_size // 2, center_y - self.square_size // 2,
                                        center_x + self.square_size // 2, center_y + self.square_size // 2)
                                self.piece_movement_enable = False
                                #print(f)
                                print(f"Got Location spot_id{newLocation}")
                            if current_location > 28:
                                piece_Moved = True
                                # Update the piece's location
                                self.canvas.itemconfig(piece, tags=tuple(tag for tag in tags if not tag.startswith("Location_")) + (f"Location_{newLocation}",))
                            
                                # Get the coordinates of the corresponding spot
                                coordinate1 = list(self.coordinate_mapping)
                                pieceLocation = coordinate1[newLocation - 1 ]
                            
                                # Calculate the center coordinates of the circle
                                center_x = pieceLocation[0] * self.square_size + self.square_size // 2
                                center_y = pieceLocation[1] * self.square_size + self.square_size // 2
                            
                                self.canvas.coords(item, center_x - self.square_size // 2, center_y - self.square_size // 2,
                                        center_x + self.square_size // 2, center_y + self.square_size // 2)
                                #self.piece_movement_enable = False
                                #print(f)
                                print(f"Got Location spot_id{newLocation}")

                            if(self.selected_turn_num.value == 3):
                                #Color(self.selected_turn_num.value  1)
                                self.selected_turn_num = Color(0) 
                            else:
                                 self.selected_turn_num = Color(self.selected_turn_num.value + 1)    
                    else:
                        #If the piece is returning home (newLocation == 0)
                        piece_Moved = True
                        #Remove the piece's current location tag
                        self.canvas.itemconfig(piece, tags = (tag for tag in tags if not tag.startswith("Location_")))
            elif f"id_{piece_Id}" not in tags:
                if f"Location_{newLocation}" in tags:
                    x = None
                    y = None
                    for tag in tags:
                        if tag.startswith("x_"):
                            x = int(tag.split("_")[1])
                            print(f"Got Location spot_id{x}")
                        if tag.startswith("y_"):
                            y = int(tag.split("_")[1])
                            print(f"Got Location spot_id{y}")
                            
                    self.canvas.itemconfig(item, tags=tuple(tag for tag in tags if not tag.startswith("Location_")) + (f"Location_{0}",))
                    # Calculate the center coordinates of the circle
                    center_x = x * self.square_size + self.square_size // 2
                    center_y = y * self.square_size + self.square_size // 2
                    self.canvas.coords(item, center_x - self.square_size // 2, center_y - self.square_size // 2,
                                    center_x + self.square_size // 2, center_y + self.square_size // 2)
                    print(f"Home spot_id{x}{y}")
                    
                        
                                
                            
                    
                    
               # Break out of the loop since we found the piece
            # if piece_Moved:
            #     {
            #         }               


    def button(self):
        button = tk.Button(self.master, text="Roll!", command=self.on_button_click)
        button.grid(row=0, column=0, padx=0, pady=0)   

    def __init__(self, master, selected_game_id, selected_turn_num):
        
        

        self.selected_game_id = selected_game_id
        self.selected_turn_num = Color(selected_turn_num)
        
        self.master = master
        self.master.title("Trouble Game Board")
        self.board_size = 19
        self.square_size = 35  # Adjust for desired square size
        self.canvas = tk.Canvas(master, width=self.board_size * self.square_size, height=self.board_size * self.square_size, bg="white")
        self.canvas.grid()
        self.canvas.bind("<Button-1>", self.on_piece_click)

        self.dice_result_label = tk.Label(master, text="Dice Roll Result: ", font=("Arial", 16, "bold"))
        self.dice_result_label.grid(row=1, column=0)
        
        #disable piece moevemnt
        self.piece_movement_enable = False
        
        #Enum for Color
        #self.colorTurn = Color()


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
            'draw_green_zone_left': "Green", 
            'draw_red_zone_left': "Red",
            'draw_red_zone_right': "Red",
            'draw_blue_zone_left': "Blue",
            'draw_blue_zone_right': "Blue",
            'draw_yellow_zone_right': "Yellow",
            'draw_yellow_zone_left': "Yellow",
            'draw_green_zone_right': "Green",

            'draw_green_home_zones': "Green",
            'draw_red_home_zones': "Red",
            'draw_blue_home_zones': "Blue",
            'draw_yellow_home_zones': "Yellow",
            
            'draw_green_starting_zones' : "Green",
            'draw_red_starting_zones' : "Red",
            'draw_blue_starting_zones' : "Blue",
            'draw_yellow_starting_zones' : "Yellow",
        }
        
        self.draw_board()
            
    def TuplePieceMover(self, circle_id):
        tags = self.canvas.gettags(circle_id)
        piece_id = None
    
        color = None
        for tag in tags:
            if "-" in tag:
                piece_id = tag.split("_")[1]
            if "color_" in tag:
                color = tag.split("_")[1]
        
        if piece_id is None:
            print ("Piece ID was not found")
            return

        print(f"Clicked on circle with ID: {circle_id}, Piece ID: {piece_id}")
    
        
        # Access the selected game ID from the ChooseGame instance
        selected_game_id = self.selected_game_id
        
        #print(self.selected_turn_num.name)
        

        
        if color == self.selected_turn_num.name:
            # Check if the game ID is selected
            if selected_game_id:
                # Send the piece movement command with the selected game ID
                signalr.hub_connection.send("MovePiece", [piece_id, selected_game_id, self.rolled_number])
                
            else:
                print("Error: No game selected.")
                      
    def on_piece_click(self, event):
        if not self.piece_movement_enable:
            messagebox.showinfo(title="Error", message="Please roll the dice first.")
            return
        
        clicked_object = self.canvas.find_closest(event.x, event.y)[0]
        if "circle" in self.canvas.gettags(clicked_object):
                self.TuplePieceMover(clicked_object)
                

                #MOVE LATER ON 
                self.piece_movement_enable = False
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
                self.canvas.itemconfig(circle, tags=("circle", f"x_{x}", f"y_{y}", f"color_{self.colors[zone_type]}", f"id_{piece_id}"))

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


