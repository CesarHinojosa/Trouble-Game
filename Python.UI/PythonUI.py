import json
from sqlite3 import Row
import tkinter as tk
from tkinter import BUTT, Button, messagebox
from dataclasses import dataclass, field, asdict
import json
import tkinter
from tkinter.tix import COLUMN
import uuid

from signalrcore.hub_connection_builder import HubConnectionBuilder

hubaddress = "https://localhost:7081/TroubleHub"
hub_connection = HubConnectionBuilder().with_url(hubaddress, options={"verify_ssl": False}).build()
hub_connection.on("ReceiveMessage", lambda msg: print("Received message back from hub." + msg[1]))
hub_connection.start()

def on_button_click():
    # Roll the dice
    user = "Cesar"
    hub_connection.send("RollDice", [user])

class LoginScreen:
    def __init__(self, master):
        self.master = master
        master.title("Login")

        self.frame = tk.Frame(master)
        self.frame.pack()

        # Creating widgets for login screen
        self.login_label = tk.Label(self.frame, text="Login", font=("Arial", 20))
        self.username_label = tk.Label(self.frame, text="Username", font=("Arial", 16) )
        self.username_entry = tk.Entry(self.frame)
        self.password_label = tk.Label(self.frame, text="Password", font=("Arial", 16) )
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
        username = "Cesar"
        password = "Hinojosa"
        if self.username_entry.get() == username and self.password_entry.get() == password:
            messagebox.showinfo(title="Login Success", message="You successfully logged in")
            self.master.withdraw()  # Hide login window
            game_window = tk.Toplevel(self.master)  # Open game window as Toplevel
            game_screen = TroubleBoard(game_window)
        else:
            messagebox.showinfo(title="Error", message="Invalid Login")

class TroubleBoard:
    def __init__(self, master):
        self.master = master
        self.master.title("Trouble Game Board")

        # Define board width and height (in squares)
        self.board_size = 19
        self.square_size = 40  # Adjust for desired square size

        # Create canvas
        self.canvas = tk.Canvas(master, width=self.board_size * self.square_size, height=self.board_size * self.square_size, bg="white")
        self.canvas.grid()

        # Define colors using a dictionary
        self.colors = {
            'safe_zone': "black",
            'draw_green_zone': "green",
            'draw_red_zone': "red",
            'draw_blue_zone': "blue",
            'draw_yellow_zone': "yellow"
        }
        # Draw game board squares
        for i in range(self.board_size):
            for j in range(self.board_size):
                x1 = i * self.square_size
                y1 = j * self.square_size
                x2 = x1 + self.square_size
                y2 = y1 + self.square_size
                self.canvas.create_rectangle(x1, y1, x2, y2, outline="black", fill="white")
                
        # Draw safe zones
        self.draw_safe_zones()  # No need to pass color as argument
        
        # Draw color zones
        self.draw_zones()
        
        self.button()

    def draw_safe_zones(self):
        # Define safe zone coordinates (center of squares)
        safe_zones = [(2, 10), (9, 3), (16, 10), (9, 17)]

        # Draw squares with a different fill color
        for zone in safe_zones:
            x, y = zone
            center_x = x * self.square_size + self.square_size // 2
            center_y = y * self.square_size + self.square_size // 2
            square_size = self.square_size // 2
            self.canvas.create_rectangle(center_x - square_size, center_y - square_size, center_x + square_size, center_y + square_size, fill=self.colors['safe_zone'], outline="black")

    def draw_zones(self):
        zones = {
            #first 6 are the board zones                                           # second set are the finish zones      # last set are the home zones
            'draw_green_zone': [(3,11), (4,12), (5,13),(5,7),(4,8),(3,9),          (3,10),(4,10),(5,10),(6,10),          (1,8),(1,9),(1,11),(1,12) ],
            'draw_red_zone': [(6,14),(7,15),(8,16),(10,16), (11,15), (12,14),      (9,13), (9,14), (9,15), (9,16),       (7,18), (8,18), (10,18), (11,18) ],
            'draw_blue_zone': [(13,13), (14,12), (15,11),(13,7), (14,8), (15,9),   (15,10),(14,10),(13,10),(12,10),      (17,8),(17,9),(17,11),(17,12)],
            'draw_yellow_zone': [(10,4), (11,5), (12,6), (8,4), (7,5), (6,6),      (9,4), (9,5), (9,6), (9,7),           (7,2), (8,2), (10,2), (11,2)]
        }
        for zone_type, zone_list in zones.items():
            for zone in zone_list:
                x, y = zone
                center_x = x * self.square_size + self.square_size // 2
                center_y = y * self.square_size + self.square_size // 2
                square_size = self.square_size // 2
                self.canvas.create_rectangle(center_x - square_size, center_y - square_size, center_x + square_size, center_y + square_size, fill=self.colors[zone_type], outline="black")

    def button(self):
        button = tk.Button(self.master, text="Roll!", command=on_button_click)
        button.grid(row=0, column=0, padx=0, pady=0)   

def main():
    root = tk.Tk()
    login_screen = LoginScreen(root)
    root.mainloop()

if __name__ == "__main__":
    main()

#packing or .grid is the best (absolute)

# hubaddress = "https://localhost:7081/TroubleHub"
# hub_connection = HubConnectionBuilder().with_url(hubaddress, options={"verify_ssl": False}).build()
# hub_connection.on("ReceiveMessage", lambda msg: print("Received message back from hub." + msg[1]))
# hub_connection.start()

# def on_button_click():
# #roll the dice  
#     #hub_connection.send("SendMessage", ["Cesar", " has connected"])
    
#     # Send a message to the RollDice method on the server
#     # Define the user and roll message
#     user = "Cesar"
#     hub_connection.send("RollDice", [user])
    
    
#     #Send a message to the Login Method on the Server
    
#     #userfails = "Cesar"
#     #passfails = "ZAqyuuB77cTBY/Z5p0b3q3+10fo="
    
#     userworks = "User1"
#     passwordworks = "Test"
    
#     #works properly
#     hub_connection.send("Login", [userworks, passwordworks])
    
#     #added Logout
#     #make sure that it works with the specific user that is logged in 
#     #hub_connection.send("Logout", [userworks])
    
 

# class TroubleBoard:
#     #need a .send for testing so it sends from Python to Console app

#     def __init__(self, master):
#         self.master = master
#         self.hub_connection = hub_connection
#         master.title("Trouble Game Board")
       
#         # Define board width and height (in squares)
#         self.board_size = 19
#         self.square_size = 40  # Adjust for desired square size

#         # Create canvas
#         self.canvas = tk.Canvas(master, width=self.board_size * self.square_size, height=self.board_size * self.square_size, bg="white")
#         self.canvas.grid()

#         # Define colors using a dictionary
#         self.colors = {
#             'safe_zone': "black",
#             'draw_green_zone': "green",
#             'draw_red_zone': "red",
#             'draw_blue_zone': "blue",
#             'draw_yellow_zone': "yellow"
#         }
#         # Draw game board squares
#         for i in range(self.board_size):
#             for j in range(self.board_size):
#                 x1 = i * self.square_size
#                 y1 = j * self.square_size
#                 x2 = x1 + self.square_size
#                 y2 = y1 + self.square_size
#                 self.canvas.create_rectangle(x1, y1, x2, y2, outline="black", fill="white")
                
#         # Draw safe zones
#         self.draw_safe_zones()  # No need to pass color as argument
        
#         # Draw color zones
#         self.draw_zones()
        
#         self.button()

#     def draw_safe_zones(self):
#         # Define safe zone coordinates (center of squares)
#         safe_zones = [(2, 10), (9, 3), (16, 10), (9, 17)]

#         # Draw squares with a different fill color
#         for zone in safe_zones:
#             x, y = zone
#             center_x = x * self.square_size + self.square_size // 2
#             center_y = y * self.square_size + self.square_size // 2
#             square_size = self.square_size // 2
#             self.canvas.create_rectangle(center_x - square_size, center_y - square_size, center_x + square_size, center_y + square_size, fill=self.colors['safe_zone'], outline="black")

#     def draw_zones(self):
#         zones = {
#             #first 6 are the board zones                                           # second set are the finish zones      # last set are the home zones
#             'draw_green_zone': [(3,11), (4,12), (5,13),(5,7),(4,8),(3,9),          (3,10),(4,10),(5,10),(6,10),          (1,8),(1,9),(1,11),(1,12) ],
#             'draw_red_zone': [(6,14),(7,15),(8,16),(10,16), (11,15), (12,14),      (9,13), (9,14), (9,15), (9,16),       (7,18), (8,18), (10,18), (11,18) ],
#             'draw_blue_zone': [(13,13), (14,12), (15,11),(13,7), (14,8), (15,9),   (15,10),(14,10),(13,10),(12,10),      (17,8),(17,9),(17,11),(17,12)],
#             'draw_yellow_zone': [(10,4), (11,5), (12,6), (8,4), (7,5), (6,6),      (9,4), (9,5), (9,6), (9,7),           (7,2), (8,2), (10,2), (11,2)]
#         }
#         for zone_type, zone_list in zones.items():
#             for zone in zone_list:
#                 x, y = zone
#                 center_x = x * self.square_size + self.square_size // 2
#                 center_y = y * self.square_size + self.square_size // 2
#                 square_size = self.square_size // 2
#                 self.canvas.create_rectangle(center_x - square_size, center_y - square_size, center_x + square_size, center_y + square_size, fill=self.colors[zone_type], outline="black")

    
    
#     def button(self):
#         button = tk.Button(self.master, text="Roll!", command=on_button_click)
#         button.grid(row=0, column=0, padx=0, pady=0)   
              


# def main():
  
    
#     root = tk.Tk()
#     app = TroubleBoard(root)
#     root.mainloop()

# if __name__ == "__main__":
#     main()




