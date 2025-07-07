
using System.Data.Common;

char[] fields = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];

bool win = false;
int player1 = 10;
int player2 = 10;

void Game()
{
    Player1();
}

void Board()
{
    Console.WriteLine("- - - - - - -  ");
    Console.WriteLine("| " + fields[0] + " | " + fields[1] + " | " + fields[2] + " |");
    Console.WriteLine("- - - - - - -  ");
    Console.WriteLine("| " + fields[3] + " | " + fields[4] + " | " + fields[5] + " |");
    Console.WriteLine("- - - - - - -  ");
    Console.WriteLine("| " + fields[6] + " | " + fields[7] + " | " + fields[8] + " |");
    Console.WriteLine("- - - - - - -  ");
}
void Player1()
{
    Board();
    while (win != true)
    {
        Console.WriteLine("Виберіть поле для ходу (гравець 1): ");
        string playerchoice = Console.ReadLine();
        int.TryParse(playerchoice, out player1);
        player1--;
        if (player1 != player2)
        {
            fields[player1] = 'X';
        }
        else
        {
            Console.Clear();
            Player1();
        }
        Console.Clear();
        Winner();
        Player2();

    }
}   

void Player2()
{
    Board();
    while (win != true)
    {
        Console.WriteLine("Виберіть поле для ходу (гравець 2): ");
        string playerchoice2 = Console.ReadLine();
        int.TryParse(playerchoice2, out player2);
        player2--;
        if (player1 != player2)
        {
            fields[player2] = 'O';
        }
        else
        {
            Console.Clear();
            Player2();
        }
        Console.Clear();
        Winner();
        Player1();

    }
}


void Winner()
{
    if (fields[0] == 'X' && fields[1] == 'X' && fields[2] == 'X' || fields[3] == 'X' && fields[4] == 'X' && fields[5] == 'X' || fields[6] == 'X' && fields[7] == 'X' && fields[8] == 'X')
    {
        Console.WriteLine("Грацець 1 (Х) виграв!");
        win = true;
    }
    else if (fields[0] == 'X' && fields[3] == 'X' && fields[6] == 'X' || fields[1] == 'X' && fields[4] == 'X' && fields[7] == 'X' || fields[3] == 'X' && fields[5] == 'X' && fields[8] == 'X')
    {
        Console.WriteLine("Грацець 1 (Х) виграв!");
        win = true;
    }
    else if (fields[2] == 'X' && fields[4] == 'X' && fields[6] == 'X' || fields[0] == 'X' && fields[4] == 'X' && fields[8] == 'X')
    {
        Console.WriteLine("Грацець 1 (Х) виграв!");
        win = true;
    }
    if (fields[0] == 'O' && fields[1] == 'O' && fields[2] == 'O' || fields[3] == 'O' && fields[4] == 'O' && fields[5] == 'O' || fields[6] == 'O' && fields[7] == 'O' && fields[8] == 'O')
    {
        Console.WriteLine("Грацець 2 (O) виграв!");
        win = true;
    }
    else if (fields[0] == 'O' && fields[3] == 'O' && fields[6] == 'O' || fields[1] == 'O' && fields[4] == 'O' && fields[7] == 'O' || fields[3] == 'O' && fields[5] == 'O' && fields[8] == 'O')
    {
        Console.WriteLine("Грацець 2 (O) виграв!");
        win = true;
    }
    else if (fields[2] == 'O' && fields[4] == 'O' && fields[6] == 'O' || fields[0] == 'O' && fields[4] == 'O' && fields[8] == 'O')
    {
        Console.WriteLine("Грацець 2 (O) виграв!");
        win = true;
    }
    if (fields[0] != '1' && fields[1] != '2' && fields[2] != '3' && fields[3] != '4' && fields[4] != '5' && fields[5] != '6' && fields[6] != '7' && fields[7] != '8' && fields[8] != '9')
    {
        Console.WriteLine("Ніч'я");
        win = true;
    }
}
Game();