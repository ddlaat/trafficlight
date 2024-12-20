@echo off
setlocal enabledelayedexpansion

REM Define the branch name to use for the full commit history
REM Replace 'master' with your branch name if necessary
set "branch=master"

REM Get the current commit hash
for /f %%C in ('git rev-parse HEAD') do set currentCommit=%%C

echo Current commit: %currentCommit%

REM Variable to indicate if the current commit has been found
set "foundCurrent="

REM Iterate through the commit history in reverse order (oldest to newest)
for /f "delims=" %%H in ('git rev-list --reverse %branch%') do (
    REM If the current commit has already been found, checkout the next commit
    if defined foundCurrent (
        git checkout %%H
        exit /b
    )

    REM Check if this commit matches the current commit
    if "%%H"=="%currentCommit%" (
        set "foundCurrent=1"
    )
)

REM If no next commit is found, notify the user
echo No next commit found.
