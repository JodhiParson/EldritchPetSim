#  Eldritch Pet Simulator

Feed your rabbit. Watch it grow. Unlock rituals.  

---

## 🧩 Overview

**Eldritch Pet Simulator** is a Unity project where you feed a mysterious rabbit to gain strength, unlock upgrades, and discover forbidden rituals.  
The core gameplay loop involves:
- Feeding the rabbit to gain **Strength**
- Upgrading feed power to increase strength per tap
- Unlocking **rituals** that affect idle gain

---

## 🛠️ How to Run the Project (Unity Source Code)

If you’ve downloaded or cloned the project folder and want to open it in **Unity**, follow these steps:

1. **Install Unity**
   - Use **Unity Hub** to manage versions.
   - This project was built with **Unity 2022.3 LTS** (or newer should work too).

2. **Open the Project**
   - In Unity Hub, click **"Open" → Add Project from Disk**.
   - Select the project folder (the one containing `Assets/`, `ProjectSettings/`, and `Packages/`).

3. **Wait for Unity to Import Assets**
   - The first time may take a few minutes as Unity compiles scripts and imports textures.

4. **Open the Main Scene**
   - Go to `Assets/Scenes/` and double-click **MainScene.unity** (or whatever your main scene is named).

5. **Play the Game**
   - Press the ▶️ **Play** button at the top of the Unity editor to run the game in the editor.
   - Or go to **File → Build Settings → Build and Run** to make a standalone build.

---

## 🧠 Notes

- Some scripts reference prefabs and assets inside the `Assets/Prefabs/`, `Assets/Scripts/`, and `Assets/UI/` folders.
- Make sure the **EventSystem** and **Canvas** are active in your scene if UI doesn’t respond.
- If sounds or animations don’t play, check that the **AudioSource** and **Animator** components are assigned in the `PetManager` inspector.

---

## 🪶 Credits

- **Design & Programming:** Jodhi Parson, Bryce Lie
- **Sprites:** Jodhi Parson, mikel
- **Engine:** Unity
- **Audio:** https://chajamakesmusic.itch.io/cute-and-silly-rpg-music-pack ; https://www.youtube.com/watch?v=yP9cYyf3vV4

---

## 📜 License

This project is provided for learning and entertainment.  
You may not redistribute or sell the assets without permission.

---

