# PlatformerGame - HRealEngine

A 3D platformer game built with **HRealEngine**, a custom C++/OpenGL game engine with C# scripting. The game features 3 levels with increasing difficulty, AI enemies driven by Behavior Trees and a Perception System, interactable objects, moveable platforms, and a timer-based scoring system.

> **Engine Repository:** [https://github.com/haktan313/HRealEngine](https://github.com/haktan313/HRealEngine)

---

## Table of Contents

- [Building & Running the Game](#building--running-the-game)
  - [Option 1: Run the Pre-Built Game (Recommended)](#option-1-run-the-pre-built-game-recommended)
  - [Option 2: Build from Source using the Editor](#option-2-build-from-source-using-the-editor)
- [How to Play](#how-to-play)
- [Game Content](#game-content)
  - [Scenes Overview](#scenes-overview)
  - [Level 1 – Introduction](#level-1--introduction)
  - [Level 2 – Vertical Platforming](#level-2--vertical-platforming)
  - [Level 3 – AI Enemy & Advanced Mechanics](#level-3--ai-enemy--advanced-mechanics)
- [Features & Systems](#features--systems)
- [Project Structure](#project-structure)
- [Screenshots](#screenshots)

---

## Building & Running the Game

### Option 1: Run the Pre-Built Game (Recommended)

https://haktan0313.itch.io/platformergame-hrealengine

1. **Download** the latest build from the `PlatformerGame_Build` folder (available on the Desktop after building, or provided separately).
2. Run **`PlatformerGame.exe`** directly — no installation needed.
3. The build folder should contain:
   - `PlatformerGame.exe` — the game executable
   - `PlatformerGame.hrpj` — project file
   - `assets/` — game assets (meshes, scenes, textures, behavior trees)
   - `Scripts/` — compiled C# script assemblies
   - `mono/` — Mono runtime files
   - `imgui.ini` — UI layout configuration

> **Important:** All files and folders must stay together in the same directory for the game to run.

### Option 2: Build from Source using the Editor

#### Prerequisites

- **Windows 10/11**
- A C++ IDE (e.g., Visual Studio 2022, Rider, etc.) with C++ and C# support
- **Git** with submodule support

#### Step 1: Clone and Build the Engine

```bash
git clone --recursive https://github.com/haktan313/HRealEngine
```

If you forgot `--recursive`:
```bash
cd HRealEngine
git submodule update --init
```

Generate project files by running **`HRealEngine/Scripts/Win-GenProjects.bat`**.

Open the generated `.sln` in your preferred IDE and build the solution in **Release** or **Debug** configuration.

#### Step 2: Clone the Game Project

```bash
git clone https://github.com/haktan313/PlatformerGame_HRealEngine
```

Place it anywhere on your machine (e.g., Desktop).

#### Step 3: Build the C# Scripts

1. Navigate to the `PlatformerGame_HRealEngine/Scripts` folder.
2. Run **`Win-GenProject.bat`** to generate the C# solution.
3. Open the generated `.sln` in your preferred IDE (Visual Studio, Rider, etc.).
4. Build the solution. The compiled `PlatformerGame.dll` will be placed in `Scripts/Binaries/`.

#### Step 4: Open the Project in the Editor

1. Launch **HRealEngine Editor**.
2. In the **Project Browser**, click **"Open Project"**.

   **Open Project**<img width="2552" height="1393" alt="OpenProjectScene" src="https://github.com/user-attachments/assets/8efb815d-e762-4efe-8a61-fb83d3809165" />

3. Navigate to the `PlatformerGame_HRealEngine` folder and select **`PlatformerGame.hrpj`**.

   **Select HRPJ**<img width="1618" height="953" alt="SelectProjectHRPJ" src="https://github.com/user-attachments/assets/fe3a185a-e43c-4d75-ace3-71395dd3334a" />

4. The editor will load the Menu scene by default.

   **Editor View**<img width="2548" height="1399" alt="BuildButton" src="https://github.com/user-attachments/assets/91fa46ca-3ed1-40a1-8ac9-b57e5d5d54b6" />

#### Step 5: Build the Game

1. In the editor, click **Build → Build Project** (or use the "Build Project" button in the top-left).
2. Select a destination folder and name the build.

   **Build Directory**<img width="2537" height="1386" alt="BuildSelectedDirectory" src="https://github.com/user-attachments/assets/256593a4-8a46-4d86-b31e-5b80d4b2e528" />

3. The engine will copy all required assets, scripts, mono runtime, and create the executable.

   **Build Output**<img width="2546" height="1397" alt="BuildFile" src="https://github.com/user-attachments/assets/a02032c4-47c9-4dcd-bc06-787aa850db1f" />

4. Navigate to the build folder and run **`PlatformerGame.exe`**.

---

## How to Play

### Controls

| Action        | Key / Input          |
|---------------|----------------------|
| Move Forward  | `W`                  |
| Move Backward | `S`                  |
| Move Left     | `A`                  |
| Move Right    | `D`                  |
| Jump          | `Space`              |
| Interact      | `E`                  |
| Menu Click    | `Left Mouse Button`  |

### Objective

Navigate through 3 levels of increasing difficulty, reaching the **"Finish"** point at the end of each level. Your total time across all levels is tracked and displayed at the end. Avoid falling off platforms (dead zones reset or reload the level), use interactable objects (keys, buttons, pushable boxes) to create paths forward, and in Level 3, use pushable boxes to block the enemy's line of sight — if it sees you and you stay too close for too long, it will shoot and kill you.

### Game Flow

```
Menu → Level 1 → Level 2 → Level 3 → End Screen
                                         ↓
                              Restart (back to Menu)
                              or Exit
```

---

## Game Content

### Scenes Overview

The game contains **6 scenes** in total:

| Scene              | Description                                      |
|--------------------|--------------------------------------------------|
| `Menu.hrs`         | Main menu with Play and Exit buttons             |
| `PlatformerLevel1.hrs` | Level 1 – Basic platforming with key interaction |
| `PlatformerLevel2.hrs` | Level 2 – Vertical platforming with moveable platforms |
| `PlatformerLevel3.hrs` | Level 3 – AI enemy, perception, pushable boxes   |
| `EndMenu.hrs`      | End screen showing total time with Restart/Exit  |
| `Level1.hrs`       | Legacy/test scene                                |

### Level 1 – Introduction

- Push the **blue box** onto the **"O" marked platform** to spawn a new platform that creates a path to the key
- Pick up the **key** by pressing `E`, this creates a **static platform** between the moveable platform and the finish, so you can reach the end
- The **moveable platform** is already active and moves side to side, but you can't jump far enough without the extra static platform
- **Dead zones** below the platforms falling reloads the level
- **Timer** displayed on screen tracking elapsed time
- Reach the **"Finish"** text to advance to Level 2

**Level 1**<img width="2552" height="1390" alt="Level1" src="https://github.com/user-attachments/assets/6185a108-249c-45fa-9633-8512a746c0af" />

### Level 2 – Vertical Platforming & Enemy

- A **moveable platform** (moving up and down) near the finish has an **enemy** standing on it
- To kill the enemy: step on a trigger platform that **destroys the moveable platform**, the enemy falls into the dead zone and dies
- Jump across to the other side, pick up the **key** with `E`, this **restores the moveable platform**
- Use the restored moveable platform to reach the **"Finish"**
- Dead zones below for fall penalties

**Level 2**<img width="2549" height="1388" alt="Level2" src="https://github.com/user-attachments/assets/5a263d39-ab57-46da-b425-93379febe52c" />

### Level 3 – AI Enemy & Advanced Mechanics

- The most complex level combining puzzles, AI, and platforming
- **Step 1:** Push the **blue box** (on the ground) onto the **"O" marked platform** to activate an up-down moveable platform
- **Step 2:** Ride the platform up to the upper area where another **blue box** sits, push it down to the lower level
- **Step 3:** The **white box** is already on the ground level near the enemy, push it to **block the enemy's line of sight**, preventing detection while you navigate past it
  - **AI Enemy** is controlled by a **Behavior Tree** with sight based perception, attack action with cooldown, and focus/unfocus behavior
- **Step 4:** Push the dropped **blue box** onto the second trigger point to activate the **platform leading to the finish**
- **Dead zones** reset box positions and kill the player/enemies
- Reach **"Finish"** to complete the game

**Level 3**<img width="2543" height="1393" alt="Level3" src="https://github.com/user-attachments/assets/90f3d020-6cba-4fb9-b98c-55a07b0cbef5" />

**Level 3 - Blocking Enemy Perception**<img width="2549" height="1390" alt="Level3 Block enemys percaption" src="https://github.com/user-attachments/assets/07259fa9-ded7-4602-b9fc-cf329dad0144" />

### End Screen

After completing all 3 levels, the end screen displays:
- **Total elapsed time** across all levels
- **Restart** button — resets timer and returns to Level1
- **Exit** button — closes the game

**End Screen**<img width="2551" height="1390" alt="EndScene" src="https://github.com/user-attachments/assets/82bbaab1-2a34-4229-b9a0-5314d31bb822" />

---

## Features & Systems

### Required Features (3D Platformer Assignment)

| Requirement | Implementation |
|---|---|
| Controllable player character | WASD movement + jump with Rigidbody3D physics, character rotates in movement direction |
| 3rd person camera | Static 3rd person camera positioned to give a clear view of the level |
| At least 3 playable levels | 3 levels with distinct designs and increasing difficulty |
| Goal, start, and platforming objects | Each level has a "Finish" goal point and platform tiles |
| Interactable objects | Keys (press E), buttons (step-on triggers), pushable boxes |
| Record time system | Timer persists across levels via GameModeData, displayed on End Screen |
| Basic menus | Custom in-engine menu with mouse-clickable buttons (Play, Exit, Restart) |
| At least one enemy | Level 3 has an AI enemy with Behavior Tree + Perception System |
| Terrain separate from static meshes | Platforms built from tile meshes, dead zones as separate collision volumes |

### Bonus Features (Nice-to-Have)

| Feature | Implementation |
|---|---|
| Moving platforms | MoveablePlatform system with start/end positions, player rides platforms correctly |
| Custom text rendering | MSDF text rendering for timer, menu text, interact prompts, and "Finish" labels |
| Custom menu system | Full menu system with mouse hover detection and scene loading |
| AI with Behavior Trees | Enemy uses BT with HasTarget condition, SetFocus, AttackAction, CooldownDecorator |
| AI Perception System | Sight-based perception detects player, loses track when line-of-sight is blocked |
| Pushable physics objects | Interactable boxes with dynamic rigidbodies, reset positions on dead zone contact |

---

## Project Structure

```
PlatformerGame_HRealEngine/
├── PlatformerGame.hrpj          # Engine project file
├── README.md                    # This file
├── assets/
│   ├── Scenes/                  # All game scenes (.hrs files)
│   │   ├── Menu.hrs
│   │   ├── PlatformerLevel1.hrs
│   │   ├── PlatformerLevel2.hrs
│   │   ├── PlatformerLevel3.hrs
│   │   ├── EndMenu.hrs
│   │   └── Level1.hrs
│   ├── Imported/                # Imported 3D assets
│   ├── Platform.hmesh           # Platform tile mesh
│   ├── Pwenguen.hmesh           # Player character mesh (penguin)
│   ├── test2.btree              # Behavior Tree asset for enemy AI
│   └── test22.btree             # Alternate Behavior Tree asset
├── Scripts/
│   ├── src/                     # C# game scripts
│   │   ├── Player.cs            # Player movement, jumping, collisions, scene transitions
│   │   ├── Enemy.cs             # Enemy AI controller with perception callbacks
│   │   ├── MenuController.cs    # Main menu logic
│   │   ├── EndMenuController.cs # End screen with time display
│   │   ├── Key.cs               # Interactable key/switch object
│   │   ├── Button.cs            # Step-on pressure button trigger
│   │   ├── MoveablePlatform.cs  # Platform that moves between two points
│   │   ├── ActivatePlatform.cs  # Trigger to activate moveable platforms
│   │   ├── DeadZone.cs          # Kill zone for player/enemies, resets boxes
│   │   ├── PlatformDestroyer.cs # Destroys/disables platforms on trigger
│   │   ├── AttackAction.cs      # BT action: enemy spawns and shoots bullet
│   │   ├── SetFocus.cs          # BT action: enemy focuses on perceived target
│   │   ├── ClearFocus.cs        # BT action: enemy clears focus
│   │   ├── HasTarget.cs         # BT condition: checks if target exists in blackboard
│   │   ├── IsInTheRange.cs      # BT condition: checks distance to player
│   │   ├── CooldownDecorator.cs # BT decorator: cooldown between attacks
│   │   ├── PlatformerAIBlackboard.cs # BT blackboard with PlayerTag and PlayerEntityID
│   │   └── BloxorzPlayer.cs     # Alternative Bloxorz-style movement (unused in final levels)
│   ├── Binaries/                # Compiled DLL output
│   └── Win-GenProject.bat       # Script to generate C# solution via Premake
└── .gitignore
```

---

## Screenshots

| Menu | Level 1 |
|------|---------|
| ![Menu](Menu.png) | ![Level 1](Level1.png) |

| Level 2 | Level 3 |
|---------|---------|
| ![Level 2](Level2.png) | ![Level 3](Level3.png) |

| Enemy Perception Blocked | End Screen |
|--------------------------|------------|
| ![Perception](Level3_Block_enemys_percaption.png) | ![End](EndScene.png) |

| Editor - Open Project | Editor - Select Project |
|-----------------------|------------------------|
| ![Open](OpenProjectScene.png) | ![Select](SelectProjectHRPJ.png) |

| Editor - Build Button | Editor - Build Output |
|-----------------------|-----------------------|
| ![Build](BuildButton.png) | ![Output](BuildFile.png) |
