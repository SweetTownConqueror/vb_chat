# vb_chat
Basic chat with a vb client and a php server

The chatVB folder contains all the visual studio files of the Tchat's client-side.
The mini-tchat folder contains the php part of the server side.

The app's functionment is the following :

-the clients open their app and are thus able to define a username, and write a message. They must replace the url by the url
where the php server side app is stored.
-when a client send a message by pressing enter or clicking the send button, a POST request is made on the php server side, containing
the pseudo, the message, and the date
-These infos are stored in a json file by the PHP script, thus allowing to make a history of all the conversation.
-The client continusouly refresh in the aim to have the very last messages from the json file that may have been uploaded by an other
client
