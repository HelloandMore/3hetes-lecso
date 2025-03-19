@echo off
pushd %~0\..
set /p selection="1. Push vagy 2. Pull > "
if %selection%==1 (
    goto push
) else (
    git pull
)

:push
git add .
git status
set /p commitMessage="Commit message: "
git commit -m "%date% - %commitMessage%"
git push
net helpmsg %errorlevel%
pause
exit /b %errorlevel%

:pull
git pull
net helpmsg %errorlevel%
pause
exit /b %errorlevel%