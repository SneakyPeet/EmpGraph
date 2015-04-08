namespace EmpGraph.Core

open System.IO
open System

module fileHelper =

    let hasExtention extension fileName = 
          Path.GetExtension(fileName) = extension

    let isZipFile = hasExtention "zip"

    let isXlsfile = hasExtention ".xls"

