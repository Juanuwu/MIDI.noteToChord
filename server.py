import flask
from flask import request,jsonify
from music21 import *

app = flask.Flask(__name__)
app.config["DEBUG"] = True



@app.route('/', methods=['GET', 'POST'])
def home():
    myChord = chord.Chord('A4 C#5 E5')

    if request.method == 'GET':
        """return the information for <user_id>"""
        
        return myChord.pitchedCommonName;
        
    if request.method == 'POST':
        """modify/update the information for <user_id>"""
        # you can use <user_id>, which is a str but could
        # changed to be int or whatever you want, along
        # with your lxml knowledge to make the required
        # changes
        data = request.data
        return data
        
        
        
        
    
        
        
    

    
    

app.run()
