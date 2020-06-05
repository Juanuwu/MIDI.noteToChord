import flask
from flask import request,jsonify
from music21 import *
from pychord import note_to_chord

app = flask.Flask(__name__)
app.config["DEBUG"] = True

global counter
counter = 0
notasActivas = []
notasNombre = []


    


@app.route('/', methods=['GET', 'POST'])
def home():
    

    if request.method == 'GET':
        """return the information for <user_id>"""
        
        return myChord.pitchedCommonName;
        
    if request.method == 'POST':
        """modify/update the information for <user_id>"""
        # you can use <user_id>, which is a str but could
        # changed to be int or whatever you want, along
        # with your lxml knowledge to make the required
        # changes
        if "c"  in request.data: 
            del notasActivas[:]
            del notasNombre[:]
            return str(len(notasNombre))
        
        
        notasActivas.append(request.data)
        
        
        for nota in notasActivas:
            
            p = pitch.Pitch(nota)
            p = p.name
            if p in notasNombre:

                test = 1
                
            else:
                notasNombre.append(p)
                
            

        str1 = ' '.join(notasNombre)
        
        for notas in notasNombre:
            print(notas)

        if str(note_to_chord(notasNombre)) == "[]":
            test = chord.Chord(notasNombre)
        
            return test.pitchedCommonName

        else:   
            myChord = note_to_chord(notasNombre)
            print(str(myChord))
        
            return str(myChord)
        




        
        #return notasActivas[counter-1]

        
        
        
    
        
        
        
        
        
    
        
        
    

    
    

app.run()
