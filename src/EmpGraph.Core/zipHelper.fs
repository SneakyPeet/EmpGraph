namespace EmpGraph.Core

open ICSharpCode.SharpZipLib.Zip
open ICSharpCode.SharpZipLib.Zip.Compression.Streams
open System.IO

module internal zipHelper =

    let private isXlsExtension fileName = 
        Path.GetExtension(fileName) = ".xls"

    let private isXls (file:ZipEntry) =
        file.IsFile && isXlsExtension file.Name 

    let getM2Mfiles stream =
        use zipFile = new ZipFile(stream = stream)
        let (files: seq<ZipEntry>) = Seq.cast zipFile
        let streams =
            files
            |> Seq.filter isXls
            |> Seq.map (fun a -> zipFile.GetInputStream(a))
            |> Seq.toList
        streams