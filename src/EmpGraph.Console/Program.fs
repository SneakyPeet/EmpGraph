open EmpGraph.Core

[<EntryPoint>]
let main argv = 
    empFiles.processEmperorM2MFiles "C:\EmpFile"
    0 // return an integer exit code
