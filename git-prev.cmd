@echo off
setlocal enabledelayedexpansion

REM Get the current commit hash
for /f %%C in ('git rev-parse HEAD') do set currentCommit=%%C

echo current commit: %currentCommit%

REM Find the next commit in reverse order
set "previousCommit="
for /f "delims=" %%H in ('git rev-list --reverse HEAD') do (
    if "%%H"=="%currentCommit%" (
        if defined previousCommit (
            REM Checkout the previous commit when the current commit is found
            git checkout !previousCommit!
            exit /b
        ) else (
            echo No previous commit found.
            exit /b
        )
    )
    REM Store the current commit in the loop as the previous commit
    set "previousCommit=%%H"
)

REM If no next commit is found, notify the user
echo No next commit found.
