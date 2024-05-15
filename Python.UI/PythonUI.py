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
import asyncio
import requests
import time
from functools import partial
from enum import Enum
from tkinter import INSERT, Canvas, messagebox
from signalrcore.hub_connection_builder import HubConnectionBuilder


class SignalR:
    #hubaddress = "https://localhost:7081/TroubleHub"
    hubaddress = "https://bigprojectapi-300077578.azurewebsites.net/troublehub";

    hub_connection = HubConnectionBuilder().with_url(hubaddress, options={"verify_ssl": False}).build()
    hub_connection.start()
    
    hub_connection.on("ReceiveMessage", lambda msg: print("Received message back from hub." + msg[1]))
    
    def send_message(self, message):
        self.hub_connection.send("SendMessage", [message])
    
signalr = SignalR()

class CreateUserScreen:
    def __init__(self, master):
        self.master = master
        master.title("Create User")
        self.frame = tk.Frame(master)
        self.frame.pack()

        # Creating widgets for create screen
        self.label = tk.Label(self.frame, text="Create a User!", font=("Arial", 16))
        self.username_label = tk.Label(self.frame, text="Username:", font=("Arial", 16))
        self.username_entry = tk.Entry(self.frame)
        self.password_label = tk.Label(self.frame, text="Password:", font=("Arial", 16))
        self.password_entry = tk.Entry(self.frame)
        self.firstname_label = tk.Label(self.frame, text="First Name:", font=("Arial", 16))
        self.firstname_entry = tk.Entry(self.frame)
        self.lastname_label = tk.Label(self.frame, text="Last Name:", font=("Arial", 16))
        self.lastname_entry = tk.Entry(self.frame)
        self.create_button = tk.Button(self.frame, text="Create User", command=self.create)

        # Grid layout for create screen widgets
        self.label.grid(row=0, column=0, columnspan=2, pady=25)  # Span across both columns
        self.username_label.grid(row=1, column=0, pady=25)
        self.username_entry.grid(row=1, column=1, pady=25)
        self.password_label.grid(row=2, column=0, pady=25)
        self.password_entry.grid(row=2, column=1, pady=25)
        self.firstname_label.grid(row=3, column=0, pady=25)
        self.firstname_entry.grid(row=3, column=1, pady=25)
        self.lastname_label.grid(row=4, column=0, pady=25)
        self.lastname_entry.grid(row=4, column=1, pady=25)
        self.create_button.grid(row=5, column=0, columnspan=2, pady=30)  # Center the button horizontally and add padding

    def create(self):
        # Implement user creation logic here
        # For example, retrieve data from entry widgets and perform necessary actions

        username = self.username_entry.get()
        password = self.password_entry.get()
        firstname = self.firstname_entry.get()
        lastname = self.lastname_entry.get()
        
        if username and password and firstname and lastname:
            signalr.hub_connection.send("CreateUser", [username, password, firstname, lastname])
            # Example: Save user data to a database or display a message box
            messagebox.showinfo("User Created", f"User '{username}' created successfully!")
            self.master.withdraw()

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
        self.create_button = tk.Button(self.frame, text="Create User", command=self.create_user)

        # Grid layout for login screen widgets
        self.login_label.grid(row=0, column=0, columnspan=2, pady=40)
        self.username_label.grid(row=1, column=0)
        self.username_entry.grid(row=1, column=1, pady=20)
        self.password_label.grid(row=2, column=0)
        self.password_entry.grid(row=2, column=1, pady=20)
        self.login_button.grid(row=3, column=0, columnspan=2, pady=30)        
        self.create_button.grid(row=4, column=0, columnspan=3, pady=30)   
        
        signalr.hub_connection.on("LoginResult", lambda msg: self.LoginResult(msg))       

    def login(self):
        #retrieve username and password by user
        username = self.username_entry.get()
        password = self.password_entry.get()

        if username and password:
            signalr.hub_connection.send("Login", [username, password])
            #signalr.hub_connection.on("LoginResult", lambda msg: self.LoginResult(print(msg)))  
           
    

    def create_user(self):
        # Open the create user screen
        self.master.withdraw()
        create_window = tk.Toplevel(self.master)
        create_screen = CreateUserScreen(create_window)
        
  
    def LoginResult(self, msg):
        if(str(msg[0]) == "True"):
            
            self.user = msg[1]
            
            print(self.user)

            #self.userId = ['Id' in msg [1]]

            messagebox.showinfo(title="Login Success", message="You successfully logged in")
            #self.master.withdraw()  # Hide login window
            options_window = tk.Toplevel(self.master)
            options_screen = OptionsScreen(options_window, self.user)
        else:
            messagebox.showinfo(title="Error", message="Invalid Login")
         
class OptionsScreen():
    def __init__(self, master, user):
        self.master = master
        master.title("Choose")

        self.frame = tk.Frame(master)
        self.frame.pack()

        # Creating widgets for Option screen
        self.option_label = tk.Label(self.frame, text="Choose an Option", font=("Arial", 20))
        
        
        self.game_button = tk.Button(self.frame, text="Choose Game", command=lambda: ChooseGame(master, user))
        

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
            choose_game_screen = ChooseGame(choose_game_window, user)
            
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
    
    def __init__(self, master, user):
        self.master = master
        self.userGuids = []
        master.title("Choose Game")
        
        self.frame = tk.Frame(master)
        self.frame.pack()

        self.user = user

        # Fetch game data from the API
        response = requests.get("https://bigprojectapi-300077578.azurewebsites.net/api/Game/GetByUser/" + self.user['Id'])
        #response = requests.get("https://localhost:7081/api/Game/GetByUser/" + self.user['Id'], verify=False)
        data = response.json()
        
        # Store game data
        self.game_data = data

        # Extract game names from the JSON data
        self.game_names = [item['gameName'] for item in data]
        self.turn_num = [item['turnNum'] for item in data]
        self.game_ids = [item['id'] for item in data]
        self.user_color = [item['userColor'] for item in data]

        # Add game names to the listbox
        self.game_label = tk.Label(self.frame, text="Select a Game", font=("Arial", 20))
        self.game_listbox = tk.Listbox(self.frame, selectmode=tk.SINGLE)
        for game_name in self.game_names:
            self.game_listbox.insert(tk.END, game_name)

        self.select_button = tk.Button(self.frame, text="Select", command=self.select_game)
        self.create_button = tk.Button(self.frame, text = "Create Game", command= self.create_game)
        self.computer_button = tk.Button(self.frame, text = "Play Computer", command=lambda: self.play_computer(user))
        
        self.game_label.pack()
        self.game_listbox.pack()
        self.select_button.pack(pady=15)
        self.create_button.pack(side=tk.LEFT, padx=15)
        self.computer_button.pack(side=tk.LEFT)

        # Instance variable to store selected game ID
        
        self.selected_game_id = None
        
    def play_computer(self,user):
        print(f"The User ID : {user['Id']}")
 
        user_color = ["Green"]
  
        signalr.hub_connection.on("CreateComputer", lambda msg: StartGame(self.master, msg[0]['Id'], msg[0]['GameName'], msg[0]['TurnNum'], user_color[0] , self.user))
        
        #I get this from the hub 
        signalr.hub_connection.send("StartComputer", [self.user['Id']])

        
    def create_game(self):
        signalr.hub_connection.send("CreateGame", [self.user['Id']])
        signalr.hub_connection.on("CreatingGame", lambda msg: self.creating_game(msg))
        signalr.hub_connection.on("CreatedGame", lambda msg: self.created_game(msg[0], msg[1]))
        
    def creating_game(self, userId):
        self.userGuids.append(userId[0])
        print(f"User Guids Count - {len(self.userGuids)}")
        
        print(f"User ID - {userId[0]}")
        if len(self.userGuids) == 4:
            signalr.hub_connection.send("CreateTheGame", [self.userGuids])

    def created_game(self, game, usergames):
        print(f"game ;{game} + usergame{usergames}")
        for usergame in usergames:
            if usergame['UserId'] == self.user['Id']:
                game['UserColor'] = usergame['PlayerColor']
        
        print("Game ID:", game['Id'])
        print("Game Name:", game['GameName'])
        print("Turn Number:", game['TurnNum'])
        print("User Color:", game['UserColor'])
        print("Username:", self.user['Username'])
        #works for computer
        #StartGame(self.master, game['Id'], game['GameName'], game['TurnNum'], game['UserColor'], self.user['Username'])
        


        StartGame(self.master, game['Id'], game['GameName'], game['TurnNum'], game['UserColor'], self.user)
           
    def select_game(self):
        selected_index = self.game_listbox.curselection()
        if selected_index:
            selected_game_index = selected_index[0]
            self.selected_game_name = self.game_names[selected_game_index]
            self.selected_game_id = self.game_ids[selected_game_index]
            self.selected_turn_num = self.turn_num[selected_game_index]
            for game in self.game_data:
                if game['gameName'] == self.selected_game_name:
                    #usercolor = self.user_color[selected_game_index]
                    #messagebox.showinfo("Selected Game", f"Game ID: {self.selected_game_id}\nGame Name: {game['gameName']}\nTurn Number: {game['turnNum']}\nGame Date: {game['gameDate']}")
                    # Start the game based on the selected game
                    StartGame(self.master,  self.selected_game_id, self.selected_game_name , self.selected_turn_num, self.user_color[selected_game_index], self.user)
                    
                    print(f"{self.selected_game_name}")
                    print(f"{self.selected_turn_num}")
                    print(f" {self.user_color}")
                    print(f" {self.user}")
                    break
        else:
            messagebox.showinfo("Error", "Please select a game.")     
            
def StartGame(master, selected_game_id, selected_game_name, selected_turn_num, user_color, user):
    master.withdraw()
    game_window = tk.Toplevel(master)
    game_screen = TroubleBoard(game_window, selected_game_id, selected_game_name, selected_turn_num, user_color, user)

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
    
    def join_game_signalr(self):
        #signalr.hub_connection.send("JoinGame", [self.user, self.selected_game_id])  
        signalr.hub_connection.send("JoinGame", [self.user['Username'], self.selected_game_id])
        
        print(f"{self.user} + {self.selected_game_id}")

    def assign_pieces_to_circles(self, circle_ids):
        response = requests.get("https://bigprojectapi-300077578.azurewebsites.net/api/PieceGame/" + self.selected_game_id)
        #response = requests.get("https://localhost:7081/api/PieceGame/" + self.selected_game_id, verify=False)
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
            if Location != 0 and Location <= 28:
                # Get the coordinates of the corresponding spot
                coordinate1 = list(self.coordinate_mapping)
                pieceLocation = coordinate1[Location - 1]
                
                # Calculate the center coordinates of the circle
                center_x = pieceLocation[0] * self.square_size + self.square_size // 2
                center_y = pieceLocation[1] * self.square_size + self.square_size // 2
                
                # Move the circle to the correct location
                self.canvas.coords(circle_id, center_x - self.square_size // 2, center_y - self.square_size // 2,
                                    center_x + self.square_size // 2, center_y + self.square_size // 2)
                
            if Location > 28:
                # Get the coordinates of the corresponding spot
               
                coordinate2 = list(self.home_mapping)

                if Color == "Green":
                    pieceLocation1 = coordinate2[Location - 29]
                if Color == "Yellow":
                    pieceLocation1 = coordinate2[Location - 25]
                if Color == "Blue":
                    pieceLocation1 = coordinate2[Location - 21]
                if Color == "Red":
                    pieceLocation1 = coordinate2[Location - 17]
                
                # Calculate the center coordinates of the circle
                center_x = pieceLocation1[0] * self.square_size + self.square_size // 2
                center_y = pieceLocation1[1] * self.square_size + self.square_size // 2
                
                # Move the circle to the correct location
                self.canvas.coords(circle_id, center_x - self.square_size // 2, center_y - self.square_size // 2,
                                    center_x + self.square_size // 2, center_y + self.square_size // 2)
                                 
    def on_button_click(self):
        print(f"{self.selected_user_color} and {self.selected_turn_num}")
        print(f"{self.selected_game_id} and {self.user['Username']}")
        if self.gameOver == False and self.selected_user_color == self.selected_turn_num.name: 
            #signalr.hub_connection.on("DiceRolled", lambda msg: self.text_dice_roll(msg))
            signalr.hub_connection.send("RollDice", [self.user['Username'], self.selected_game_id])
            print(f"{self.selected_user_color[0]}")
            
    def on_button_skip(self):
        
        if self.selected_turn_num.name == self.selected_user_color or self.computer_game and self.selected_turn_num.name != self.selected_user_color:
            if self.rolled_number != 6:
                signalr.hub_connection.send("SkipTurn", [self.selected_game_id])
            elif self.rolled_number == 6:
                    self.dice_result_label.config(text=f"Roll Again!!", font=("Arial", 24, "bold"))
                    if self.computer_game == True and self.selected_user_color != self.selected_turn_num.name:
                        time.sleep(2)
                        signalr.hub_connection.send("RollDice", ['Computer', self.selected_game_id])
           
    def next_color(self):
        # Increment the turn to the next player's turn
        if self.selected_turn_num.value == 3:
            self.selected_turn_num = Color(0)  # Reset to the first player if it's the last player's turn
        else:
            self.selected_turn_num = Color(self.selected_turn_num.value + 1)
            
        if self.computer_game and self.selected_turn_num.name != self.selected_user_color:
            signalr.hub_connection.send("RollDice", ['Computer', self.selected_game_id])
       
    def text_dice_roll(self, msg):
        
        #print("Hello World")
        if self.computer_game and self.selected_turn_num.name != self.selected_user_color:
            time.sleep(2)
        # Convert the integer to a string
        result_str = str(msg[0])
        
        #Update the label text
        color_turn = self.selected_turn_num.name
        # Update the label text with the rolled dice result
        self.dice_result_label.config(text=f"Color Turn: {color_turn}, Rolled a: {result_str}", font=("Arial", 24, "bold"))
        self.rolled_number = msg[0]
        
        #Enable piece movement after the dice is rolled 
        self.piece_movement_enable = True
        
        if self.computer_game and self.selected_turn_num.name != self.selected_user_color:
            signalr.hub_connection.send("ComputerTurn", [self.selected_game_id, self.selected_turn_num.name, self.rolled_number])
        #print(f"Selected Turn Num: {self.selected_turn_num.name}")
        
    def check_winner(self):
            home_counts = {'Red': 0, 'Yellow': 0, 'Blue': 0, 'Green': 0}
            
            for item in self.canvas.find_withtag("circle"):
                tags = self.canvas.gettags(item)
                # if f"id_{piece_Id}" in tags:
                    #Found the piece with the specfic ID
                    #piece = item
                    #piece location from its tags
                current_location = None
                color = None
                #print(f"got Piece")
                for tag in tags:
                    if tag.startswith("Location_"):
                        current_location = int(tag.split("_")[1])
                        #print(f"Got Location spot_id: {current_location}")
                    if tag.startswith("color_"):
                        color = Str(tag.split("_")[1])
                        #print(f"Got color:  {color.value}")
                if current_location > 28:
                    home_counts[color.value] += 1
                    # print(f"{home_counts.values}")
                if home_counts[color.value] == 4:
                    # Display a message box indicating the winner
                    messagebox.showinfo("Winner!", f"{color.value} is the winner!")
                    self.gameOver = True
                    break
                    #return True  # Indicates that a winner has been found  
                   
            #print(f"{home_counts['Red']}")
            #print(f"{home_counts['Yellow']}")
            #print(f"{home_counts['Blue']}")
            #print(f"{home_counts['Green']}")
    
    def move_piece_return(self, piece_Id, newLocation):
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
                color = None
                print(f"got Piece")
                for tag in tags:
                    if tag.startswith("Location_"):
                        current_location = int(tag.split("_")[1])
                        print(f"Got Location spot_id: {current_location}")
                    if tag.startswith("color_"):
                        color = Str(tag.split("_")[1])
                        print(f"Got color:  {color.value}")
                print(f"Got Location spot_id: {newLocation}")
                if current_location is not None:
                    #if the piece is moving to a new location 
                    if newLocation != 0:
                        if current_location != newLocation:
                            if newLocation <= 28:
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
                                print(f"The new location spot will be spot_id{newLocation}")
                            if newLocation > 28:
                                piece_Moved = True
                                # Update the piece's location
                                self.canvas.itemconfig(piece, tags=tuple(tag for tag in tags if not tag.startswith("Location_")) + (f"Location_{newLocation}",))
                                
                                coordinate2 = list(self.home_mapping)

                                if color.value == "Green":
                                    pieceLocation1 = coordinate2[newLocation - 29]
                                if color.value == "Yellow":
                                    pieceLocation1 = coordinate2[newLocation - 25]
                                if color.value == "Blue":
                                    pieceLocation1 = coordinate2[newLocation - 21]
                                if color.value == "Red":
                                    pieceLocation1 = coordinate2[newLocation - 17]
 
                                # Calculate the center coordinates of the circle
                                center_x = pieceLocation1[0] * self.square_size + self.square_size // 2
                                center_y = pieceLocation1[1] * self.square_size + self.square_size // 2
                            
                                self.canvas.coords(item, center_x - self.square_size // 2, center_y - self.square_size // 2,
                                        center_x + self.square_size // 2, center_y + self.square_size // 2)
                                self.piece_movement_enable = False
                                print(f"{coordinate2}")
                                print(f"Got Location spot_id{newLocation}")
                                print(f"newLocation worked thingy")

                                self.check_winner()
                                
                            if self.rolled_number != 6:
                                #time.sleep(2)
                                self.next_color()
                                

                            elif self.rolled_number == 6:
                                 self.dice_result_label.config(text=f"Roll Again!!", font=("Arial", 24, "bold"))
                                 if self.computer_game == True and self.selected_user_color != self.selected_turn_num.name:
                                     time.sleep(2)
                                     signalr.hub_connection.send("RollDice", ['Computer', self.selected_game_id])
                                              
                    else:
                        #If the piece is returning home (newLocation == 0)
                        piece_Moved = True
                        #Remove the piece's current location tag
                        self.canvas.itemconfig(piece, tags = (tag for tag in tags if not tag.startswith("Location_")))
            elif f"id_{piece_Id}" not in tags:
                if f"Location_{newLocation}" in tags:
                    if newLocation <= 28:
                        x = None
                        y = None
                        for tag in tags:
                            if tag.startswith("x_"):
                                x = int(tag.split("_")[1])
                                print(f"Got Location spot_id:  {x}")
                            if tag.startswith("y_"):
                                y = int(tag.split("_")[1])
                                print(f"Got Location spot_id:  {y}")
                            
                        self.canvas.itemconfig(item, tags=tuple(tag for tag in tags if not tag.startswith("Location_")) + (f"Location_{0}",))
                        # Calculate the center coordinates of the circle
                        center_x = x * self.square_size + self.square_size // 2
                        center_y = y * self.square_size + self.square_size // 2
                        self.canvas.coords(item, center_x - self.square_size // 2, center_y - self.square_size // 2,
                                        center_x + self.square_size // 2, center_y + self.square_size // 2)

    def button(self):
        button = tk.Button(self.master, bg = "black", fg="white", height=1, width=12,  text="Roll!", command=self.on_button_click)
        button.grid(row=0, column=0, padx=0, pady=0)  
        
    def __init__(self, master, selected_game_id, selected_game_name, selected_turn_num, selected_user_color, user):
        self.master = master
        self.selected_game_id = selected_game_id
        self.selected_game_name = selected_game_name
        self.selected_turn_num = Color(selected_turn_num)
        self.selected_user_color = selected_user_color
        self.user = user
        
        signalr.hub_connection.on("Skip", lambda msg: self.next_color())
        signalr.hub_connection.on("DiceRolled", lambda msg: self.text_dice_roll(msg))

        self.join_game_signalr()

        self.computer_game = False
        if(selected_game_name == "ComputerGame"):
            self.computer_game = True

            if self.selected_user_color != self.selected_turn_num.name:
                signalr.hub_connection.send("RollDice", ['Computer', self.selected_game_id])
                  
            #time.sleep(5)
            signalr.hub_connection.on("ComputerReturn", lambda msg:(
                time.sleep(3), signalr.hub_connection.send("MovePiece", [msg[0], self.selected_game_id, self.rolled_number])))
            
            #print(f"{self.selected_game_id}")
            #print(f"{self.selected_game_id}")
            #print(f"{self.rolled_number}")

            signalr.hub_connection.on("ComputerMoveFail", lambda msg: self.on_button_skip())
            
            #hello
        
        
        print(f"Selected Game ID is: {selected_game_id}")
        
        self.gameOver = False
        
       
        self.master.title("Trouble Game Board")
        self.board_size = 19
        self.square_size = 35  # Adjust for desired square size
        self.canvas = tk.Canvas(master, width=self.board_size * self.square_size, height=self.board_size * self.square_size, bg="white")
        self.canvas.grid()
        self.canvas.bind("<Button-1>", self.on_piece_click)

        self.dice_result_label = tk.Label(master, text="Dice Roll Result: ", font=("Arial", 24, "bold"))
        self.dice_result_label.grid(row=1, column=0)
        
        self.skipbutton = tk.Button(master, text="Skip Turn", command=self.on_button_skip, bg = "black", height=2, width=15, fg="white", font=15)
                                    
        self.skipbutton.grid(row=3, column=0)
        
        signalr.hub_connection.on("MovePieceReturn", lambda msg: self.move_piece_return(msg[0], msg[1]))
        
        signalr.hub_connection.on("JoinGame", [user, selected_game_id])

        #disable piece moevemnt
        self.piece_movement_enable = False

        self.button()
        #self.skipbutton()

        #need this to store the ID of the tuple in a dictionary 
        #this is for the spots around 
        self.coordinate_mapping = {}  #Dictionary to store mapping of tuples to integers GAME ZONES   
        self.current_id = 1  # Start the ID from 1
        
        self.home_mapping = {}
        self.home_id = 1 
        for i in range(self.board_size):
            for j in range(self.board_size):
                x1 = i * self.square_size
                y1 = j * self.square_size
                x2 = x1 + self.square_size
                y2 = y1 + self.square_size
                self.canvas.create_rectangle(x1, y1, x2, y2, outline="black", fill="white")
               
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
        self.check_winner()
            
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
        if self.gameOver == False and self.selected_user_color == self.selected_turn_num.name:
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
            'draw_green_home_zones': [(4,10),(5,10),(6,10),(7,10)],
            
            'draw_yellow_home_zones' : [(9,5),(9,6),(9,7),(9,8)],
            
            'draw_blue_home_zones' : [(14,10),(13,10),(12,10),(11,10)],
            
            'draw_red_home_zones' : [(9,15),(9,14),(9,13),(9,12)],  
        }     
     
        # Call TupleFinder to get the coordinates for each zone
        # game_zone_coordinates = TupleFinder(game_zones)
        # starting_zone_coordinates = TupleFinder(starting_zones)
        # home_zone_coordinates = TupleFinder(home_zones)
        
        # print("Game Zone Coordinates:", game_zone_coordinates)
        # print("Starting Zone Coordinates:", starting_zone_coordinates)
        # print("Home Zone Coordinates:", home_zone_coordinates)
        
        #print("Coordniate Mapping game_zones :", self.coordinate_mapping)
        
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
       
  
        for zone_type, zone_list in home_zones.items():
            for i, home_zone in enumerate(zone_list, start=1):
                x, y = home_zone
                
                self.home_mapping[(x, y)] = self.home_id
                self.home_id += 1
                home_id = self.home_mapping[(x, y)]

                # Just draws it
                center_x = x * self.square_size + self.square_size // 2
                center_y = y * self.square_size + self.square_size // 2
                square_size = self.square_size // 2
                self.canvas.create_rectangle(center_x - square_size, center_y - square_size, center_x + square_size,
                                                center_y + square_size, outline=self.colors[zone_type], tags=("home_id"))    
                self.canvas.create_text(center_x, center_y, text=str(home_id), font=("Arial", 10, "bold"))
                
        print("Coordniate Mapping home_zones :", self.home_mapping)     
        
        self.assign_pieces_to_circles(circle_ids)
        
               
def main():    
    root = tk.Tk()
    login_screen = LoginScreen(root)
    root.mainloop()

if __name__ == "__main__":
    main()


