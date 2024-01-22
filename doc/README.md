# Alpha 2
### Developer: Maksym Kintor C4b
### E-mail: `kintor@spsejecna.cz`
### Date: 10.01.2024

## Architecture:
<pre>
|___Alpha_Two/
    |___Alpha_Two/
        |___bin/
            |___Debug/
                |___net6.0/
                    |___Alpha_Two.exe (RUN THIS)
        |___data/
            |___decodedOutput.txt
            |___dictionary.txt
            |___input.txt
            |___output.txt
            |___possibleSigns.txt
        |___doc
            |___README.md (this file)
        |___log/
            |___log.txt
        |___src/
            |___commands/
                |___CompressCommand.cs
                |___DecompressComand.cs
                |___HelpCommand.cs
                |___ICommand.cs
                |___LogCommand.cs
            |___logic/
                |___Compressor.cs
                |___Decompressor.cs
                |___Logger.cs
            |___Program.cs
        |___Alpha_Two.csproj
        |___App.config
    |___Alpha_Two.sln/
    
</pre>

## Run Project
Navigate to folder `Alpha_Two` with CMD.  
Run with `Alpha_Two\bin\Debug\net6.0\Alpha_Two.exe` or double-click on exe file

## Config file
Navigate to `Alpha_Two\Alpha_Two\App.config`  
You are able to adjust:
- dictionaryFilePat" default value="../../../data/dictionary.txt"/>
- logFilePath default value="../../../log/log.txt"/>
- dictionaryFilePath default value="../../../data/dictionary.txt"/>
