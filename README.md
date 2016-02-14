# Jedi Knight VR
Zoe Naidoo
Zakir Gau
Andoni Garcia

## Description
Jedi Knight is a virtual reality game developed for Google Cardboard. We use two phones -- one for the VR headset and another to simulate a handheld lightsaber -- in order to create an interactive real-time lightsaber dualing game.

This project was developed for our University of Chicago class, CMSC 23400 - Mobile Computing - Winter 2016.

## Installation

We rely on Git Large File Storage to handle to large assets involved in the Unity development. As such, if you are using a Mac operating system, you can install git-lfs via `brew install git-lfs`. Else, you can always go to [their website](https://git-lfs.github.com) in order to install the binary code.

Next, you need to link git to Large File Storage with the following:

```
git lfs install
```

Finally, you need to clone the repo via:

```
git clone https://github.com/andonigarcia/JediKnight.git
```

Now you can begin developing! If you add a new type of asset that needs to be put under LFS control, simply track it via

```
git lfs track "*.<asset_type>"
```

where `<asset_type>` can be anything from a `PSD` to an `MP4` file. After you put the files under LFS tracking, you can use git as normal!

Cheers.