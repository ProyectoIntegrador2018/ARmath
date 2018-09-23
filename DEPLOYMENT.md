## Prerequisites

1. Have the latest version of Vuforia installed alongside Unity 2018.2 or later.
2. Have at leats Unity 2018.2 or later.
3. Have a runnable project for Unity 2018.2 or later.
4. For iOS deployment you must have, at least, a valid Apple ID and the latest version of Xcode installed.
4. For iOS and Android deployment you must have installed the iOS Build Support for Unity and Android sdk.

## Setting up Project for Deployment 

_Step 1:_ Inside your Unity project go to  `Edit->Project Settings->Player`.
_Step 2:_ For both iOS and Android go to `XR Settings` and enable  `Vuforia Augmented Reality`.
_Step 3:_  For iOS go to  `Other Settings`  and set a valid Bundle Identifier.
_Step 4:_ For Android go to `Other Settings` and set a valid Package Name.

## iOS Deployment 
_Step 1:_ Go to `Edit->Build Settings` select iOS and click on `Switch Target`.
_Step 2:_ Finally, click on `Build` and selec the directory where you want to store the XcodeProject.
_Step 3:_ Open the Xcode Project that was generated.
_Step 4:_ On the general setting of the Xcode Project, set a Team and Provision Profile for the application.
_Step 5:_ Now you can run the application on either a simulator or an actual device.

## Android Deployment 
_Step 1:_ Go to `Edit->Build Settings` select Android and click on `Switch Target`.
_Step 2:_ Finally, click on `Build` and selec the directory where you want to store the apk that is goind to be generated.
_Step 5:_ Now you can run the application on an actual device by downloading the apk file.
