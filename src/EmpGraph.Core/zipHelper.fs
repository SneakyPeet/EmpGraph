namespace EmpGraph.Core

open ICSharpCode.SharpZipLib.Zip
open ICSharpCode.SharpZipLib.Zip.Compression.Streams
open System.IO

module internal zipHelper =

    let processZipItem item:ZipEntry =
        

    let getM2MfileStream =
        let stream = new MemoryStream()
        use zipFile = new ZipFile(stream)
        zipFile |> processZipItem |> ignore
        let entry = zipInputStream.GetNextEntry()
        entry.ge