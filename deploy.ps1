(get-service -ComputerName andev-bld01 -Name XmppBot).Stop()

. .\build.ps1

if($LASTEXITCODE -ne 0 -and $LASTEXITCODE -ne $null ){
    write-host "ERROR" -foregroundcolor "white" -backgroundcolor "red"
    Exit 1
}

robocopy .\XmppBot\bin\Debug\plugins \\andev-bld01\XmppBot\plugins /MIR

(get-service -ComputerName andev-bld01 -Name XmppBot).Start()