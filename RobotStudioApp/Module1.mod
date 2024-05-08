MODULE Module1
    CONST robtarget pHome:=[[181.694820364,-12.467961259,373.289368071],[0.001826271,0.004541362,0.999740988,0.022226053],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget p1:=[[481.615903006,3.198526215,202.904763246],[0.001826257,0.004541398,0.999740989,0.022226029],[0,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget p2:=[[115.9454973,-347.386091524,202.904758055],[0.001826247,0.004541368,0.999740989,0.022226029],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget p3:=[[127.740301411,321.656292249,202.904661641],[0.00182625,0.004541352,0.999740988,0.022226055],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    VAR socketdev serverSocket;
    VAR socketdev clientSocket;
    VAR string data;

    PROC main()

        SocketCreate serverSocket;
        SocketBind serverSocket,"127.0.0.1",4000;
        SocketListen serverSocket;
        SocketAccept serverSocket,clientSocket,\Time:=WAIT_MAX;

        MoveJ pHome,v600,fine,tool0\WObj:=wobj0;

        WHILE TRUE DO

            SocketReceive clientSocket,\Str:=data,\Time:=WAIT_MAX;

            IF data="POS1" THEN
                MoveJ p1,v600,fine,tool0\WObj:=wobj0;
                SocketSend clientSocket,\Str:="Robot in position 1";
            ELSEIF data="POS2" THEN
                MoveJ p2,v600,fine,tool0\WObj:=wobj0;
                SocketSend clientSocket,\Str:="Robot in position 2";
            ELSEIF data="POS3" THEN
                MoveJ p3,v600,fine,tool0\WObj:=wobj0;
                SocketSend clientSocket,\Str:="Robot in position 3";
            ELSEIF data="HOME" THEN
                MoveJ pHome,v600,fine,tool0\WObj:=wobj0;
                SocketSend clientSocket,\Str:="Robot in Home position";
            ELSE
                SocketSend clientSocket,\Str:="Wrong command";
            ENDIF

        ENDWHILE

        WaitTime 1;

    ERROR

        IF ERRNO=ERR_SOCK_TIMEOUT THEN
            RETRY;

        ELSEIF ERRNO=ERR_SOCK_CLOSED THEN
            SocketClose clientSocket;

            SocketClose serverSocket;
            SocketCreate serverSocket;
            SocketBind serverSocket,"127.0.0.1",4000;
            SocketListen serverSocket;
            SocketAccept serverSocket,clientSocket,\Time:=WAIT_MAX;

            RETRY;

        ELSE
            stop;
        ENDIF

    ENDPROC

ENDMODULE