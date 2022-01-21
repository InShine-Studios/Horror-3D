# Astralization Project

This article contains the general information for this project including the development team, project structures, and other important stuff.

<a name="toc"></a>
## Table of Contents

> 1. [General Info](#general-info)
> 2. [Development Team](#dev-team)
> 3. [_Developers](#dev)
> 4. [Project Hierarchy](#project)
> 5. [Asset Naming Conventions (ANC)](#anc)
> 6. [Scene Structure](#scene)

<a name="general-info"></a>
## 1. General Information

Astralization is blablabla ...

Tech Stack: Unity 2020.3.25f1 (Active Since: 19 Dec 2021)

**[⬆ Back to Top](#table-of-contents)**



<a name="dev-team"></a>
## 2. Dev Team
- Raihansyah Attallah Andrian (Chieftain)

### Programmer Clan
- Rafif Taris (Chief Programmer)
- Naufal Hilmi Irfandi

### Designer Clan
- Gusti Ngurah Yama Adi Putra (Chief Designer and Artist)
- Vadimos Bhirawa

### Artist Clan
- Ahmad Haydar Alkaf
- Jahns Michael
- Raihan Rizqi Muhtadiin

**[⬆ Back to Top](#table-of-contents)**



<a name="dev"></a>
## 3. _Developers

This folder is used for **DEVELOPMENT** purposes. All local scenes and assets that you want to use during development should be kept here. Keep all new assets in your own subfolder under your name. After finishing your local development, move **ONLY** the assets that you want to integrate with the game to the corresponding folder.

> You can still push all local assets to GitHub since it should not conflict with any other assets. Integrating the new asset to the actual level can be done separately.

Example Cases:

**Ben wants to develop a new decoration "stove" prefab that can turn on and off.**

1. Create or copy an existing scene on your local folder "Ben/".
2. Create the prefab on your local folder "Ben/".
3. Create materials used for that prefab also in "Ben/". You can add your own directory "Ben/Stove".
4. Create a script called "StoveController" to turn the stove on and off.
5. Add flame particle assets for the stove.
6. Move all final assets to the corresponding folder. In this case it should be inside "Gameplay/Decorations" folder. Moved assets include M_HO_Stove, MAT_XXX, MAT_YYY, PS_Stove_Fire, etc.

> Add tests if needed.


**Sarah wants to add new audios for the "stove" Ben just finished working on.**

1. Copy Ben's stove scene.
2. Import the new audio to local folder "Sarah/".
3. Integrate the audio system with the "Stove" object in a new "Stove Prefab".
4. Add the "M_HO_Stove" mesh to the prefab.
5. Integrate the added audio through the script.
6. Move all final assets to the corresponding folder. In this case it should be inside "Audios" for the audio files and inside "Gameplay/Decorations" for the new prefab.

> Add tests if needed.


**Alex wants to integrate stove to a Map Scene**

1. Copy the Map Scene to local folder "Alex/".
2. Add Sarah's stove prefab (as needed). Remember to keep the scene hierarchy clean!
3. Check all functionality still runs the same.
4. Move the new scene to the original folder, add a suffix to the original name - e.g. "Asylum_Stoves".

> Tests should not be added for this.

**[⬆ Back to Top](#table-of-contents)**



<a name="project"></a>
## 4. Project Hierarchy

All project files used are in the "Assets/Astralization" folder.

### Sections

> 4.1 [_Levels](#level)

> 4.2 [Audios](#audio)

> 4.3 [Character](#char)

> 4.4 [FX](#fx)

> 4.5 [Gameplay](#gameplay)

> 4.6 [Scripts](#script)

> 4.7 [Tests](#test)

> 4.8 [UI](#ui)

<a name="level"></a>
### 4.1 _Levels

Folder to keep **ALL working levels** in the game. This includes the general assets used, frontend (menus, splash screen, etc.), maps, etc.

**[⬆ Back to Top](#table-of-contents)**


<a name="audio"></a>
### 4.2 Audios

Folder to keep **ALL audio assets** in the game. This includes audio classes (AudioManager, etc.), unused audios, mixers, sources, etc.

> Note: Only general audio files or those that have not been integrated should be kept here.

**[⬆ Back to Top](#table-of-contents)**


<a name="char"></a>
### 4.3 Characters

Folder to keep **ALL character related assets** in the game. This includes prefabs, spritesheets, animations, etc. If there are common assets to be used for all characters, keep them in a folder such as `Characters/Common/Animations`.

**[⬆ Back to Top](#table-of-contents)**


<a name="fx"></a>
### 4.4 FX

Folder to keep **ALL working effects** in the game. This includes volumes, particle system, textures, etc.

**[⬆ Back to Top](#table-of-contents)**


<a name="gameplay"></a>
### 4.5 Gameplay

The main folder to keep **ALL gameplay related assets**. This includes objects related to gameplay or world building. Examples are interactables, items, decorations, etc.

**[⬆ Back to Top](#table-of-contents)**


<a name="script"></a>
### 4.6 Scripts

This folder is used to keep **ALL the scripts** that will be used inside the project. It also has its own **assembly definition** asset to exclude it from the normal C# assembly for testing purposes. 

Each script will be kept in its own subfolder depending on what it is used for. The name of the script file **must** be the same as the class name. All scripts should be named using **PascalCase** format.

**[⬆ Back to Top](#table-of-contents)**


<a name="test"></a>
### 4.7 Tests

The folder that will be used to run both **unit** (edit mode) and **integration** (play mode) tests. Each test class will be kept under a subfolder specific to the use of that test scenario.

List of All [Test Cases](https://docs.google.com/spreadsheets/d/1G74XV_d05xa5i4_eDIRdUH0l6cRDMIJ_D6vSRm2YQrQ/edit?usp=sharing)

**[⬆ Back to Top](#table-of-contents)**


<a name="ui"></a>
### 4.8 UI

This folder is used to keep **ALL the assets** used for UI. User interface includes objects in game (marker, objective, etc.) or menu UI.

**[⬆ Back to Top](#table-of-contents)**


<a name="anc"></a>
## 5. Asset Naming Conventions

Naming conventions should be treated as law. A project that conforms to a naming convention is able to have its assets managed, searched, parsed, and maintained with incredible ease.

Most things are prefixed with the prefix generally being an acronym of the asset type followed by an underscore.

Naming Convention: `Prefix_BaseAssetName_Variant_Suffix`

All assets should have a _Base Asset Name_. A Base Asset Name represents a logical grouping of related assets. Any asset that is part of this logical group 
should follow the the standard of  `Prefix_BaseAssetName_Variant_Suffix`.

Keeping the pattern `Prefix_BaseAssetName_Variant_Suffix` in mind and using common sense is generally enough to warrant good asset names.

### Sections

> 5.1 [Animations](#anc-animation)

> 5.2 [Audios](#anc-audio)

> 5.3 [Effects](#anc-effect)

> 5.4 [Materials](#anc-material)

> 5.5 [Meshes](#anc-mesh)

> 5.6 [Textures](#anc-texture)

> 5.7 [UI](#anc-ui)

<a name="anc-animation"></a>
### 5.1 Animations

Assets that are **animations** that will be used in the project. Divided as several types such as animation clips, controllers, etc.

Examples:
- A_Iris_Idle_S
- AC_Char05
- A_Door_Opening

| Asset Type           | Prefix | Suffix | Notes |
| -------------------- | ------ | ------ | ----- |
| Animation Clip       | A_     |        |       |
| Animation Controller | AC_    |        |       |
| Avatar Mask          | AM_    |        |       |
| Morph Target         | MT_    |        |       |

**[⬆ Back to Top](#table-of-contents)**


<a name="anc-audio"></a>
### 5.2 Audios

Assets that are **audios** that will be used in the project. Divided based on the audio type. Full detail on naming conventions can be seen in the **QS List and SDD**.

Examples:
- MUS_HouseAbigail_1_Loop
- SFX_Gameplay_InanimateObject_DoorClose_1

| Asset Type     | Prefix | Suffix | Notes |
| -------------- | ------ | ------ | ------|
| Music          | MUS_   |        |       |
| Sound Effects  | SFX_   |        |       |
| Stingers       | STG_   |        |       |
| Voiceover      | VO_    |        |       |
| Ambiences      | AMB_   |        |       |
| Audio Mixer    | MIX_   |        |       |

List of All [Audios](https://docs.google.com/spreadsheets/d/1gsHRRFwN4QyYJzZvs9QmzklRW7wCB5ivEE4ZGU4QDK4/edit?usp=sharing)

**[⬆ Back to Top](#table-of-contents)**


<a name="anc-effect"></a>
### 5.3 Effects

Assets that are **effects** that will be used in objects or scenes. This includes volumes, particle systems, etc.

| Asset Type      | Prefix | Suffix | Notes |
| --------------- | ------ | ------ | ----- |
| VOL             | VOL_   |        |       |
| Particle System | PS_    |        |       |

**[⬆ Back to Top](#table-of-contents)**


<a name="anc-material"></a>
### 5.4 Materials

Assets that are **materials** that will be used in objects. Materials can both be the original material, material instance, or a physical material. Since materials can have variants for the same object, material names should specify both the part and variant of the material: **[Prefix]\_[ObjectName]\_[Part]\_[VariantNum]**.

Examples:
- MAT_Fog_1
- MAT_LightSwitch_Base_1

| Asset Type        | Prefix | Suffix | Notes |
| ----------------- | ------ | ------ | ----- |
| Material          | MAT_   |        |       |
| Material Instance | MI_    |        |       |
| Physical Material | PM_    |        |       |

**[⬆ Back to Top](#table-of-contents)**


<a name="anc-mesh"></a>
### 5.5 Meshes

Assets that are **3D Original Meshes**. These original meshes will not have each variation for all materials that can be used for each mesh. Using a different Material can be implemented in a scene directly (less prefabs to be kept in the project).
Use **"M_"** as the prefix for meshes.

Examples:
- M_CH_Iris
- M_LS_CeilingLight_TMP

| Asset Type    | Prefix | Suffix | Notes |
| ------------- | ------ | ------ | ----- |
| Light Sources | LS_    |        |       |
| House Objects | HO_    |        |       |
| Temporary     |        | _TMP   |       |

List of All [Meshes](https://docs.google.com/spreadsheets/d/1uGEMDAMG0gg0ye39r2UNICfS8tpuOsGJXS7MlWKUVZk/edit?usp=sharing)

**[⬆ Back to Top](#table-of-contents)**


<a name="anc-texture"></a>
### 5.6 Textures

Assets that are **original textures** such as spritesheets, render textures, or any other textures.
Use **"T_"** as the prefix for textures.

Examples:
- T_BG_MainMenu
- T_OBJ_Door_Wooden_1

#### Naming

| Asset Type     | Prefix      | Suffix | Notes |
| ---------------| ----------  | ------ | ----- |
| Background     | BG_         |        |       |
| Character      | CH_         |        |       |
| Object         | OBJ_        |        |       |
| Particle       | P_          |        |       |
| Render         | R_          |        |       |
| UI             | UI_         |        |       |

#### Character Sprites

For each character sprite, each frame must be named with this convention: **[CharName]\_[AnimActivity]\_[Direction]\_[OrderNum]**.
If there are no directions or number used (only one sprite) then empty the missing field.

Examples:
- Iris_Walk_W_1
- Iris_Idle_N
- RockMan_Idle_3

| Direction               | Suffix     |
| ----------------------- | ---------- |
| North                   | N          |
| South                   | S          |
| West                    | W          |
| East                    | E          |
| North East              | NE         |
| North West              | NW         |
| South East              | SE         |
| South West              | SW         |

List of All [Textures](https://docs.google.com/spreadsheets/d/1POtREJ4ZyqbAkOQF2Zi1HseyXMY9iGUI_oE2QhcA9Y8/edit?usp=sharing)

**[⬆ Back to Top](#table-of-contents)**


<a name="anc-ui"></a>
### 5.7 UI

Assets that are **UI components** that will be used in the project. It includes fonts, art, etc.

| Asset Type       | Prefix | Suffix | Notes |
| ---------------- | ------ | ------ | ----- |
| Font             | Font_  |        |       |
| Texture (Sprite) | T_UI_  |        |       |

**[⬆ Back to Top](#table-of-contents)**



<a name="scene"></a>
## 6. Scene Structure

Next to the project's hierarchy, there's also scene hierarchy. This is only a  template example. You can adjust it to your needs. Scenes hould be kept only in _Developers folder or _Levels folder.

<pre>
Debug
Management
UI
Cameras
Lights
World
    Terrain
    Props
Gameplay
	Actors
	Items
_Dynamic
</pre>

 - All empty objects should be located at 0,0,0 with default rotation and scale.
 - For empty objects that are only containers for scripts, use "@" as prefix - e.g. @Cheats
 - When you're instantiating an object in runtime, make sure to put it in _Dynamic - do not pollute the root of your hierarchy or you will find it difficult to navigate through it.

**[⬆ Back to Top](#table-of-contents)**