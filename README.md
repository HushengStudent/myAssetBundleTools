# myAssetBundleTools
Unity5.x assetbundle tools.

software：Unity5.3.8/visual studio2013.

**1、BuildAssetBundles(string outputPath, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform)：**

所有设置了AssetBundle Name的资源会打成AssetBundle，未设置AssetBundle Name的资源会根据依赖关系(包括直接依赖和间接依赖)打包到被依赖的资源的AssetBundle中，所以要注意不需要打包的资源的冗余问题。

如：

①APrefab设置了AssetBundle Name为a.assetbundle，APrefab依赖于BPrefab(未设置AssetBundle Name)，BPrefab依赖于CPrefab(未设置AssetBundle Name)，使用该函数打包，则APrefab、BPrefab与CPrefab都会被打包到a.assetbundle中。

②APrefab设置了AssetBundle Name为a.assetbundle，APrefab依赖于BPrefab(设置AssetBundle Name为a.assetbundle)，BPrefab依赖于CPrefab(未设置AssetBundle Name)，使用该函数打包，则APrefab会被打包到a.assetbundle中，BPrefab与CPrefab都会被打包到b.assetbundle中。

**2、BuildAssetBundles(string outputPath, AssetBundleBuild[] builds, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform)：**

该打包方法与AssetBundle Name无关。

①APrefab依赖于BPrefab，当AssetBundleBuild[]只打打包APrefab时，BPrefab也会被打到该AssetBundle中，与BPrefab是否设置了AssetBundle Name无关。当BPrefab需要单独打包时，AssetBundleBuild[]应同时包含APrefab与BPrefab。

**3、GetAllDependencies(string assetBundleName)和GetDirectDependencies(string assetBundleName)**

会根据依赖关系获取信息，与资源存在于哪个AssetBundle文件无关。