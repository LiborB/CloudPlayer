rem Change host name and directory and then rename this file as build-deploy-frontend.cmd (to gitignore)

npm run build --prefix CloudPlayer.Client/client

scp -r CloudPlayer.Client/client/dist/* <host>:<directory>

pause