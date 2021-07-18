# Unity3D-Utils
This package regroups various utility tools used in the studio

## Install Package
This package uses the scoped registry feature to import dependent packages.
Please add the following sections to the package manifest file (Packages/manifest.json).

To the scopedRegistries section:
```
{
    "name": "Bonjour-lab",
    "url": "https://registry.npmjs.com",
    "scopes": [
    "com.bonjour-lab"
    ]
}
```

To the dependencies section:

```
"com.bonjour-lab.utils": "0.0.2",
```

After changes, the manifest file should look like below:
```
{
  "scopedRegistries": [
    {
      "name": "Bonjour-lab",
      "url": "https://registry.npmjs.com",
      "scopes": [
        "com.bonjour-lab"
      ]
    }
  ],
  "dependencies": {
    "com.bonjour-lab.utils": "0.0.2",
    ...
```

