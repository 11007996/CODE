@echo off
call npx tsc --target es2015 only-ts.ts
call node only-ts.js