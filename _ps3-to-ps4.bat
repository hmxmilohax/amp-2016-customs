cd "%~dp0\songs"
forfiles /S /M *.*_ps3 /C "cmd /c rename @file @fname.*_ps4"