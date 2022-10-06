<div>

# [VUdon](https://github.com/Varneon/VUdon) - Depth Buffer Toolkit [![GitHub](https://img.shields.io/github/license/Varneon/VUdon-DepthBufferToolkit?color=blue&label=License&style=flat)](https://github.com/Varneon/VUdon-DepthBufferToolkit/blob/main/LICENSE) [![GitHub Repo stars](https://img.shields.io/github/stars/Varneon/VUdon-DepthBufferToolkit?style=flat&label=Stars)](https://github.com/Varneon/VUdon-DepthBufferToolkit/stargazers) [![GitHub all releases](https://img.shields.io/github/downloads/Varneon/VUdon-DepthBufferToolkit/total?color=blue&label=Downloads&style=flat)](https://github.com/Varneon/VUdon-DepthBufferToolkit/releases) [![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/Varneon/VUdon-DepthBufferToolkit?color=blue&label=Release&sort=semver&style=flat)](https://github.com/Varneon/VUdon-DepthBufferToolkit/releases/latest)

</div>

A toolkit for configuring the depth buffer in VRChat worlds with Udon

# Usage

> 1. Navigate to `Varneon` > `VUdon` > `DepthBufferToolkit` > `Add Runtime Activator To Scene`
>
> ![image](https://user-images.githubusercontent.com/26690821/190958151-d3d08127-9002-4039-a46a-2f56f3394491.png)

> 2. Click `Add all cameras from the current scene` and `Add all mirrors from the current scene` depending on which ones you want to enable the depth buffer on
>
> ![image](https://user-images.githubusercontent.com/26690821/190958569-81ec4936-a51d-4b8c-862e-706aed819e20.png)

> 3. Depth Buffer Activator will now activate the depth buffer on the objects using the following methods:
> * `Main Camera`: Temporary Post-process Volume with DoF on the first frame
> * `Other Cameras`: Directly set the DepthTextureMode property of the camera to Depth
> * `Mirrors`: Attach MirrorDepthBufferActivator component during build, which finds the internal camera of the mirror when it's rendered for the first frame, then set the DepthTextureMode property of the internal camera to Depth

# Installation

<details><summary>

### Import with [VRChat Creator Companion](https://vcc.docs.vrchat.com/vpm/packages#user-packages):</summary>

> 1. Download `com.varneon.vudon.depth-buffer-toolkit.zip` from [here](https://github.com/Varneon/VUdon-DepthBufferToolkit/archive/refs/heads/main.zip)
> 2. Unpack the .zip somewhere
> 3. In VRChat Creator Companion, navigate to `Settings` > `User Packages` > `Add`
> 4. Navigate to the unpacked folder, `com.varneon.vudon.depth-buffer-toolkit` and click `Select Folder`
> 5. `VUdon - Depth Buffer Toolkit` should now be visible under `Local User Packages` in the project view in VRChat Creator Companion
> 6. Click `Add`

</details><details><summary>

### Import with [Unity Package Manager (git)](https://docs.unity3d.com/2019.4/Documentation/Manual/upm-ui-giturl.html):</summary>

> 1. In the Unity toolbar, select `Window` > `Package Manager` > `[+]` > `Add package from git URL...` 
> 2. Paste the following link: `https://github.com/Varneon/VUdon-DepthBufferToolkit.git?path=/Packages/com.varneon.vudon.depth-buffer-toolkit`

</details><details><summary>

### Import from [Unitypackage](https://docs.unity3d.com/2019.4/Documentation/Manual/AssetPackagesImport.html):</summary>

> 1. Download latest `com.varneon.vudon.depth-buffer-toolkit.unitypackage` from [here](https://github.com/Varneon/VUdon-DepthBufferToolkit/releases/latest)
> 2. Import the downloaded .unitypackage into your Unity project

</details>
