import pychord 
import music21 
global counter 
counter = 0 
test = "A"
notasActivas = [] 
notasNombre = []
if "c" in test:  
   del notasActivas[:]
   del notasNombre[:]
   result = str(len(notasNombre))
notasActivas.append(test)



for nota in notasActivas:
   p = nota
                   
   if p in notasNombre:
       test = 1
   else:
       notasNombre.append(p)
                    

if str(pychord.analyzer.note_to_chord(notasNombre)) == "[]":
   test5 = music21.chord.Chord(notasNombre)
   result = test5.pitchedCommonName
else:
   myChord = pychord.analyzer.note_to_chord(notasNombre)
   result = str(myChord)


print(result)
