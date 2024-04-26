import tkinter as tk
from functools import partial
from tkinter import messagebox
from signalrcore.hub_connection_builder import HubConnectionBuilder

hubaddress = "https://localhost:7081/TroubleHub"

hub_connection = HubConnectionBuilder().with_url(hubaddress, options={"verify_ssl": False}).build()
hub_connection.on("ReceiveMessage", lambda msg: print("Received message back from hub." + msg[1]))
hub_connection.start()



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
            hub_connection.send("Login", [userworks, passwordworks])
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
        self.game_button = tk.Button(self.frame, text="Start Game", command=lambda: StartGame(master))
        self.logout_button = tk.Button(self.frame, text="Log Out", command=lambda: LogOut(master))

        #Grid Layout
        self.option_label.grid(row=0, column=0, columnspan=2, pady=40)
        self.game_button.grid(row=1, column=0)
        self.logout_button.grid(row=1, column=1, pady=20)   
                     
#hit database
def LogOut(master):
    user = "User1"
    hub_connection.send("Logout",[user])
    master.withdraw()
    
    login_window = tk.Toplevel(master)
    login_screen = LoginScreen(login_window)
    



#-----------------------------------------Game Aspect-------------------------------------

def StartGame(master):
    master.withdraw()
    game_window = tk.Toplevel(master)
    game_screen = TroubleBoard(game_window)

def on_button_click():
    # Roll the dice
    user = "User1"
    hub_connection.send("RollDice", [user])
    
def TupleFinder(zones):
        coordinates = []
        for zone_type, zone_list in zones.items():
            for coordinate in zone_list:
                coordinates.append(coordinate)
        return coordinates
       

class TroubleBoard:
    def button(self):
        button = tk.Button(self.master, text="Roll!", command=on_button_click)
        button.grid(row=0, column=0, padx=0, pady=0)

    def __init__(self, master):
        self.master = master
        self.master.title("Trouble Game Board")
        self.board_size = 19
        self.square_size = 35  # Adjust for desired square size
        self.canvas = tk.Canvas(master, width=self.board_size * self.square_size, height=self.board_size * self.square_size, bg="white")
        self.canvas.grid()
        self.canvas.bind("<Button-1>", self.on_piece_click)
        
        #need this to store the ID of the tuple in a dictionary 
        #this is for the spots around 
        self.coordinate_mapping = {}  # Dictionary to store mapping of tuples to integers
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
        self.button()
        
    def reset_ids(self):
            self.coordinate_mapping = {}
            self.current_id = 1
            # Redraw the board
            self.draw_board()
        

    def TuplePieceMover(self, circle_id):
            # Retrieve the piece ID and color of the clicked circle
            #piece_id = self.canvas.itemcget(circle_id, "tags").split(" ")[0]
            
            color = self.canvas.itemcget(circle_id, "fill")
            
            #method for piece_id and game_id using JSON
            piece_id = "0e041e67-51c3-4d6e-a7b8-3684c5a9a793"
            game_id = "c225c4f3-f378-467b-9722-7c5852cb584e"
            
           

            # Print the information about the clicked circle
            #print(f"Clicked on circle with ID: {circle_id}, Piece ID: {piece_id}, Color: {color}")

            # Send the information to the server or perform any other necessary actions
            hub_connection.send("MovePiece", [piece_id, game_id, 1])
            

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
                 else:
                     print("Clicked on empty space")
            

    # def on_piece_click(self, event):
    #     piece_id = "0e041e67-51c3-4d6e-a7b8-3684c5a9a793"  # Example piece ID
    #     game_id = "c225c4f3-f378-467b-9722-7c5852cb584e"  # Example game ID
    #     #print(f"Clicked at coordinates: ({x}, {y})")
    #     # Send SignalR message to move the piece
    #     hub_connection.send("MovePiece", [piece_id, game_id, 1])  # Assuming 1 space to move


    # def on_piece_click(x , y):
        
    #     piece_id = "0e041e67-51c3-4d6e-a7b8-3684c5a9a793"
                
    #     # Assuming the game ID is stored somewhere accessible
    #     game_id = "c225c4f3-f378-467b-9722-7c5852cb584e"
    #             
    #     print(f"Clicked at coordinates: ({x}, {y})")

    #     # Send SignalR message to move the piece
    #     hub_connection.send("MovePiece", [piece_id, game_id, 1])  # Assuming 1 spaces to move 
        
   
    def draw_board(self):
        game_zones = {
            
            'draw_green_zone_right': [(3,10),(3, 11), (4, 12), (5, 13)], 
            
            'draw_red_zone_left': [(6, 14), (7, 15), (8, 16), (9,16)], 
            
            'draw_red_zone_right': [ (10, 16), (11, 15), (12, 14)],
            
            'draw_blue_zone_left': [ (13, 13), (14, 12), (15, 11), (15,10)],
            
            'draw_blue_zone_right': [(15, 9), (14, 8), (13, 7)],
            
            'draw_yellow_zone_right': [(12,6), (11,5), (10,4), (9,4)],
            
            'draw_yellow_zone_left' : [(8, 4), (7, 5), (6, 6)],
            
            'draw_green_zone_left': [(5,7), (4,8), (3,9)]
                                
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
        game_zone_coordinates = TupleFinder(game_zones)
        starting_zone_coordinates = TupleFinder(starting_zones)
        home_zone_coordinates = TupleFinder(home_zones)

        print("Game Zone Coordinates:", game_zone_coordinates)
        print("Starting Zone Coordinates:", starting_zone_coordinates)
        print("Home Zone Coordinates:", home_zone_coordinates)

             
       #spots on the game board
        for zone_type, zone_list in game_zones.items():
            for game_zone in zone_list:
                #this code adds the ID to the specific tuple
                x, y = game_zone
                self.coordinate_mapping[(x, y)] = self.current_id
                self.current_id += 1
                spot_id = self.coordinate_mapping[(x, y)]
                
                #print(f"Spot at ({x}, {y}) has ID: {spot_id}")
                

                #just draws it
                center_x = x * self.square_size + self.square_size // 2
                center_y = y * self.square_size + self.square_size // 2
                square_size = self.square_size // 2
                self.canvas.create_rectangle(center_x - square_size, center_y - square_size, center_x + square_size,
                                              center_y + square_size, fill=self.colors[zone_type], outline="black")    
                
                self.canvas.create_text(center_x, center_y, text=str(spot_id), font=("Arial", 10, "bold"))
                

        #cicle pieces
        for zone_type, zone_list in starting_zones.items():
            for i, starting_zone in enumerate(zone_list, start=1):
                x, y = starting_zone
        
                # Add piece ID to the coordinate mapping
                self.coordinate_mapping[(x, y)] = self.current_id
                self.current_id += 1
                piece_id = self.coordinate_mapping[(x, y)]
        
                # Draw circular piece
                center_x = x * self.square_size + self.square_size // 2
                center_y = y * self.square_size + self.square_size // 2
                radius = self.square_size // 2
        
                # Create a circle and tag it as "circle" to identify it later
                circle = self.canvas.create_oval(center_x - radius, center_y - radius, center_x + radius,
                                                  center_y + radius, fill=self.colors[zone_type], outline="Orange", tags="circle")
        
                text = f"{i}"  # Example: "1"
                self.canvas.create_text(center_x, center_y, text=text, font=("Arial", 5, "bold"))
                

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
    
    # def reset_game():
    #     TroubleBoard.reset_ids() 
        
    root = tk.Tk()
    login_screen = LoginScreen(root)
    root.mainloop()

if __name__ == "__main__":
    main()

