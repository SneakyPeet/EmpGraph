namespace EmpGraph.Core

open System.IO
open System

type fileInput = {name:string; file:MemoryStream}

type M2MDetails = { account:string; date:DateTime; openValue:decimal; closeValue:decimal; deposit:decimal; change:decimal}
