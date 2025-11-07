
# <h1 align=center> 🤿 DESCENT : ABYSSAL PLUMMET 🤿 <br/> Harrison Traue

<h3 align=center> <img src="Averysaidtorenamethisimage.png" width="350">


### Gameplay and elements
-------------------------    

https://github.com/user-attachments/assets/ae580dbe-157b-4bfb-b74e-825bb6fc0b8c





## What I have delivered and what went well :star:

- A playable character can move and jump
- An obstacle with collider, which is able to kill the player
- A respawn mechanic 
- Multiple Functional particle systems
- Implementation of lights, manipulating the shadows
- A following camera with delayed follow speed  
- 1 Short level design
- A title screen animation, playing into the title screen, which proceeds into the main menu screen
- Sound tracks to main menu and Title screen sequence 
- A functional main menu UI screen, with Options, Quit and play.
- Implemented an how to play UI before Level 2 begins. Click any key to begin level 2
- A pause panel UI screen (LEVEL 2)
- Working resume button (LEVEL 2)
- A functional restart feature to restart the level again (LEVEL 2)
- In game real time score UI (LEVEL 2)
- Display of in game combo UI (LEVEL 2)
- Animated background gifs (LEVEL 2)
- Working Rhythm beatscroller - Function : Allows the notes to fall (LEVEL 2)
- Hit detection of the Turtle (Arrows) prefabs (LEVEL 2)
- Hidden Cursor while playing, and pausing re-enables it (LEVEL 2)
- Next level sequence if score threashold reached (LEVEL 2)





**I am really proud of how my game turned out, from the basic 2d platformer to visually appealing rhythm game. I feel like I have excelled in creating a vision on how I wanted my game to look - Aquatic. I managed to build a solid core game loop, successfully implementing the BeatScroller for note movement and a stacked GameManager.cs to handle complex state transitions like pausing and game over. I was estactic when the score system worked, holding multiple score threshholds. I focused on polishing the game by adding player friendly features pause panel, resume and restart buttons. I also included combo text and background music to further enchance visual feedback. Most of my time was focused on developing the rhythm game (Or level 2) so level 1 had minimal code, and mostly focused on visual elements. The simple title scene sequence was something I was also proud of.**





## What I had hoped to deliver and what didn't go well :x:

- A working Combo system (Increasing score with higher combos) (LEVEL 2)
- Expansion of Level 1 - Pause panel, restart button, Full level design, Nice visual elements, Player animation etc
- Development of the ending levels - Google snake like game, and round based boss battle minigame
- Addition of settings in the options menu - Adjustable sound, screen size, etc
- A guide npc which would swim past your screen, giving basic game tips
- An actual storyline to the game, animated cutscene by hand in unity using the animator
- Synced sound for the beginning cutscene
- Paralex background for Level 1



**While the core aesthetic and structure of my rhythm game was nearly complete. My most critical failure was in inability to fix the Combo and score multiplier system, which made the game feel less rewarding in a way. Content in level 1 is lacking. And it fails to introduce my actual game and its storyline. I also had some critical errors with the intial setup of the [beatscroller](https://github.com/TempeHS/2025CT_Harrison.T_name/blob/main/Assets/Scripts/BeatScroller.cs) code, as the manual method of alligning notes was too much of a problem.** 





# About my project

## General

Descent - Abyssal Plummet, Is the name of my game. The aim of this game is to progress through deep sea themed levels completing a variety of Minigames that I had hoped to design, with the finale of the game being a deep sea brawl against a fierce crab. Plot wise, It needs work however while developing I had mostly drifted off course and did whatever I thought was fun to do, or whatever I invested my thoughts in to.

## Objective and Idea

Descent : Expedtion. To play simple hold the A & D keys to move either left or right until you reach the end!.  This first level is a simple 2D platformer consisting of obstacles, atmosphere and low gravity...   

Descent : Jellyfish drop. To play simply tap the falling jellyfish precisely as they align with the turtles at the bottom of the screen. As the music plays, jellyfish will descend, and your goal is to tap each lane right when its jellyfish perfectly overlaps its corresponding target. The game ends once the music has stopped. To succeed, listen to the music as its your guide.  This is the 2nd level of the adventurous dive. Developement is inspired by multiple rhythm games already existing on the internet. Popular games include, 'Rhythm plus', 'Friday Night Funkin', 'Osu Mania' and quite the handful more. Gameplay is practically identical, however I have implemented a few of my own twists to match my overall theme. 


### Controls / Keybinds ⌨️

Descent : Expedition           
-------------------------      
| Function  | Keybind  |        
| ---       | ---      |        
| Walk Right|  D / ➡️  |         
| Walk Left |  A / ⬅️  |        
| Jump      | SpaceBar |         


Descent : Jellyfish Drop
---------------------------
| Function |  Keybind  |
| -------------------- |-------------------|
| Left / Yellow Turtle | D / ⬅️            |
| Up / Blue Turtle     | F / ⬆️            |
| Down / Green Turtle  | J / ⬇️            |
| Right / Red Turtle   | K / ➡️            | 
| Pause Panel          | Escape key        |


## Gameplay  🎮 

#### Main menu

When you first launch Descent : Abyssal plummet, you are greeted with a title animation sequence which eventually leads you into the main menu. Which serves as the home page. There is a play button. An options button, which has no function as it lacks settings. And a functional quit button to exit the game. This main design is heavily inspired off the [tutorial](https://www.youtube.com/watch?v=DX7HyN7oJjE&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=23) design, with the exception of using my own sprites.

<br />
<br />
<br />

<img width="851" height="471" alt="Image" src="https://github.com/user-attachments/assets/d945294d-4132-4be7-8ff8-d555a248d7a7" />


<br />
<br />
<br />

#### Level 1

Descent : Expedition. My first level. This was the foundation of my game. The first thing someone first starting unity would do. Go through the basic [movement](https://github.com/TempeHS/2025CT_Harrison.T_name/blob/main/Assets/Scripts/PlayerMovement.cs) tutorials, set up a [camera](https://github.com/TempeHS/2025CT_Harrison.T_name/blob/main/Assets/Scripts/CameraController.cs.meta) that follows you. Implementation of [enemies or obstacles](https://github.com/TempeHS/2025CT_Harrison.T_name/blob/main/Assets/Scripts/GameController.cs), introduction of tile palette, maybe play around with particles and light. Thats exactly what I had done. Made my own *simple* 2D platforming game. 

<br />
<br />
<br /> 

<img width="1086" height="391" alt="Image" src="https://github.com/user-attachments/assets/80ef0f1e-6a19-49eb-9ea3-dd2fc8cd2662" />

<br />
<br />
<br />

I had shortly altered paths to develop something more of my interest which will be shown in Level 2. Shown in the image below is a demonstration of the use of 2D light. Understanding in particle emmitting. And the usage of tile palettes. 

<img width="1086" height="413" alt="Image" src="https://github.com/user-attachments/assets/42c68450-a49c-465d-9ce3-10092c43cfd3" />

<br />
<br />
<br />


#### Level 2


Descent : Jellyfish drop utilizes the four standard keys for rhythm game input, mapping directly to the four lanes where notes descend. Lane 1 (left) is hit with the D or LEFT ARROW key, Lane 2 with F or UP ARROW key, Lane 3 with J or DOWN ARROW key, and the final Lane 4 (right) with K or RIGHT ARROW KEY. With every successful hit, a bubbly explosion particle effect plays where the note has hit. Each of these lanes possess a turtle which acts as the Receiver which reacts upon player input, which registers the hits of the [note objects](https://github.com/TempeHS/2025CT_Harrison.T_name/blob/main/Assets/Scripts/NoteObject.cs). 

<br />
<br />
<br />



<img width="745" height="414" alt="Image" src="https://github.com/user-attachments/assets/c7233413-a7c8-41cd-9197-6084973cf4a2" />



<br />
<br />
<br />




The Score tracks your total points, updating upon every successful hit or miss. There is a combo counter however there is no working code backing it up so it is only there for the looks. Score increases by 89 per hit, and decreases by -113 to further increase difficulty. All notes must be hit as they pass the invisible Hit Line at the bottom of the track which is marked by the turtles. If the ESC key is pressed, the Pause Menu appears, offering options to Resume (after a brief 3-second countdown), Restart the song, or return to the Main Menu. Most of this is found in [the GameManager script.](https://github.com/TempeHS/2025CT_Harrison.T_name/blob/main/Assets/Scripts/GameManager.cs)

<br />
<br />
<br />


<img width="786" height="442" alt="Image" src="https://github.com/user-attachments/assets/790ffa6d-d2cb-4597-87b9-854a6b415d46" />










## Development Documentation

I have used Visual studio code to edit and write my scripts. 
I have then used unity as my video game editor.

<br />

<img width="1919" height="1031" alt="Image" src="https://github.com/user-attachments/assets/b9782204-7f0c-4064-b0d8-d6c410a44fc8" />

<img width="1918" height="1047" alt="Image" src="https://github.com/user-attachments/assets/6e41bd03-94e9-4cca-819b-384c60be8679" />



## Asset Showcase :open_file_folder: 
-------------------------    

[View all Assets](https://github.com/TempeHS/2025CT_Harrison.T_name/tree/main/Assets/Sprites)

Every 2D element **except** for the smoke cloud and main menu background has been produced by the *Artist* (Me)

## Tutorials


[Rhythm game tutorial Part 1](https://www.youtube.com/watch?v=cZzf1FQQFA0&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv&index=1) 

[Rhythm game tutorial Part 2](https://www.youtube.com/watch?v=PMfhS-kEvc0&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv&index=2)

[Rhythm game tutorial Part 3](https://www.youtube.com/watch?v=dV9rdTlMHxs&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv&index=3)

[Rhythm game tutorial Part 4](https://www.youtube.com/watch?v=dV9rdTlMHxs&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv&index=4)

[Rhythm game tutorial Part 5](https://www.youtube.com/watch?v=Usuh7WUAPbg&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv&index=5)

[2D LEVEL DESIGN with Tile Palette](https://www.youtube.com/watch?v=vN4H7N_k3eg&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=3)

[2D SMOOTH CAMERA FOLLOW Tutorial](https://www.youtube.com/watch?v=QfLhSzeZaoA&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=4)

[2D Player RESPAWN Tutorial](https://www.youtube.com/watch?v=odStG_LfPMQ&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=10)

[MAIN MENU Quickly! | Unity UI Tutorial ](https://www.youtube.com/watch?v=DX7HyN7oJjE&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=23)

[2D Player Movement In Unity](https://www.youtube.com/watch?v=K1xZ-rycYY8)
[How to Import a 2D Character Sprite Sheet and Use in a GameObject in Unity](https://www.youtube.com/watch?v=FXXc0hTWIMs)

[Idle, Run and Jump Animations - Platformer Unity 2D](https://www.youtube.com/watch?v=Sg_w8hIbp4Y&t=315s)

[How To Make An Atmospheric 2D Game](https://www.youtube.com/watch?v=7qcKMUioPI0)

[How To Make An Atmospheric 2D Game (Pt2)](https://www.youtube.com/watch?v=S10eaYrNnYM)

[]()

[]()
[]()
[]()
[]()
[]()
[]()



I have utilzed ai in assisting me throughout this project. I used Co-Pilot to accelerate the learning curve for complex unity systems. The AI acted as a mentor, assisting me for the Combo system, the Pause State and some visual systems. I have made sure to use Ai as a tool to Strengthen my understanding of Unity and C# code, instead of it being a tool to replace myself.












# Other


## Authors :ledger:

🎨 Artist - Harrison Traue
💻 Developer - Harrison Traue

ex. Mr Jones
ex. [@benpaddlejones](https://github.com/benpaddlejones)

## License :credit_card:

This project is licensed under the [HARRISON TRAUE] License - see the LICENSE.md file for details

