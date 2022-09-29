cd "%~dp0\songs"
forfiles /S /M *.*_ps4 /C "cmd /c rename @file @fname.*_ps3"