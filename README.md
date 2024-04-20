## Use a Sample

You can find the list of all the samples in the Unity package manager window.
Click on "Design Pattern" package, then go to the "Samples" section and import the samples you are interesting in.


## Add a Package

- Create a folder in the "Samples~" folder with an explicit name according to the design pattern or architecture you implement.
- Update the "package.json" file, add a new entry in the "samples" section like this :

```
{
    "displayName": "sampleName",
    "description": "",
    "path": "Samples~/sampleFolder"
}
```

- Then in the "package.json" file, update the version according the following rules.

Version : x.y.z

Increment Y when you add a new package.
Increment Z when you update a package.

- In "CHANGELOG.md" file, describe the modifications made compared to the previous version.
