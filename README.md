# openlawnz-word-plugin

This is a VSTO plugin because the new Office API [does not have access to footnotes](https://officespdev.uservoice.com/forums/224641-feature-requests-and-feedback/suggestions/33088741-support-for-search-api-in-headnotes-and-footnotes)

## Running

Go into the project properties and generate a certificate under the Signing tab.

## Publishing

```
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild" <path/to/sln/file> /target:publish /p:SignManifests=true /p:ManifestCertificateThumbprint=<thumbprint/from/certificate> /p:Configuration=Release;PublishDir=<publish/directory> /p:SignAssembly=true /p:AssemblyOriginatorKeyFile=<path/to/certificate/file>
```

Note: Your path to MSBuild may vary

For distribution you will need a Code Signing certificate from a place like [Digicert](https://www.digicert.com/code-signing/microsoft-authenticode.htm)