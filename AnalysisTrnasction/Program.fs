open System

type Transaction = { 
    payer: string; 
    receive: string; 
    amount: int; 
}

type Block = { 
    index: int; 
    timestamp: DateTime; 
    data: Transaction; 
    previousHash: string; 
    hash: string; 
}

let getHash block = 
    let rawData = 
        block.index.ToString() + 
        block.timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff") + 
        block.data.payer + 
        block.data.receive + 
        block.data.amount.ToString() + 
        block.previousHash
    use sha256 = System.Security.Cryptography.SHA256.Create()
    let dataBytes = System.Text.Encoding.UTF8.GetBytes(rawData)
    let hashBytes = sha256.ComputeHash(dataBytes)
    BitConverter.ToString(hashBytes).Replace("-", "").ToLower()

let createGenesisBlock = 
    let transaction = { payer = ""; receive = ""; amount = 0 }
    { index = 0; timestamp = DateTime.UtcNow; data = transaction; previousHash = "0"; hash = getHash { index = 0; timestamp = DateTime.UtcNow; data = transaction; previousHash = "0"; hash = "" } }

let createNewBlock previousBlock transaction =
    let newIndex = previousBlock.index + 1
    let newTimestamp = DateTime.UtcNow
    { index = newIndex; timestamp = newTimestamp; data = transaction; previousHash = previousBlock.hash; hash = getHash { index = newIndex; timestamp = newTimestamp; data = transaction; previousHash = previousBlock.hash; hash = "" } }

let blockchain = ref [createGenesisBlock]

let addTransaction transaction =
    let latestBlock = List.last !blockchain
    let newBlock = createNewBlock latestBlock transaction
    blockchain := !blockchain @ [newBlock]

let printBlock block =
    printfn "Index: %d" block.index
    printfn "Timestamp: %O" block.timestamp
    printfn "payer: %s" block.data.payer
    printfn "receive: %s" block.data.receive
    printfn "Amount: %d" block.data.amount
    printfn "Previous Hash: %s" block.previousHash
    printfn "Hash: %s\n" block.hash

let printBlockchain () =
    List.iter printBlock !blockchain

let transaction1 = { payer = "Alice"; receive = "Bob"; amount = 10 }
let transaction2 = { payer = "Bob"; receive = "Charlie"; amount = 5 }
let transaction3 = { payer = "Charlie"; receive = "Alice"; amount = 3 }

addTransaction transaction1
addTransaction transaction2
addTransaction transaction3

printBlockchain ()