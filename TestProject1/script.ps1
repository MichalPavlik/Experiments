[cmdletbinding()]
param(
   [switch]$DebugMessages
)

$ErrorActionPreference="Stop"
$ProgressPreference="SilentlyContinue"

Write-Output "Doing something important"

if ($DebugMessages)
{
	Write-Output "Debug!!: Debugging message"
}

throw "Oh no!"