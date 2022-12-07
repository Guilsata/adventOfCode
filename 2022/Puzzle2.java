import java.util.ArrayList;

public class Puzzle2 {
    //Part 1/ A X rock 1
    //Part 1/ B Y Paper 2
    //Part 1/ C Z scissors 3
    //Part 1/ lose 0
    //Part 1/ draw 3
    //Part 1/ win  6

    //Part 2/ X lose 
    //Part 2/ Y draw 
    //Part 2/ Z win  

    public static void main(String args[]){
        ArrayList<String> file = UtilsFile.myReadFile("data2.txt");
        int result=0;
        String[] tmp;
        for(String line : file){
                tmp = line.split(" ");   
                result = result+game(tmp[0],tmp[1]);
        }
        System.out.println(result);
    }

    private static int game(String foe, String wanted){
        switch(wanted){
            case "X":
                return gameLoose(foe);
            case "Y":
                return gameDraw(foe);
            default:
                return gameWin(foe);
        }
    } 

    private static int gameLoose(String foe){
        switch(foe){
            // ROCK
            case "A":
                return game2(foe,"Z");
            // PAPER    
            case "B":
                return game2(foe,"X");
            // SCISSORS
            default:
                return game2(foe,"Y");
        }
    }

    private static int gameDraw(String foe){
        switch(foe){
            // ROCK
            case "A":
                return game2(foe,"X");
            // PAPER    
            case "B":
                return game2(foe,"Y");
            // SCISSORS
            default:
                return game2(foe,"Z");
        }
    }

    private static int gameWin(String foe){
        switch(foe){
            // ROCK
            case "A":
                return game2(foe,"Y");
            // PAPER    
            case "B":
                return game2(foe,"Z");
            // SCISSORS
            default:
                return game2(foe,"X");
        }
    }

    private static int game2(String foe, String you){
        switch(you){
            case "X":
                return gameRock(foe).points + 1;
            case "Y":
                return gamePaper(foe).points + 2;
            default :
                return gameScissors(foe).points + 3;
        }
    } 

    private static Result gameRock(String foe){
        switch(foe){
            case "A":
                return Result.DRAW;
            case "B":
                return Result.LOSE;
            default:
                return Result.WIN;
        }
    }

    private static Result gamePaper(String foe){
        switch(foe){
            case "A":
                return Result.WIN;
            case "B":
                return Result.DRAW;
            default:
                return Result.LOSE;
        }
    }

    private static Result gameScissors(String foe){
        switch(foe){
            case "A":
                return Result.LOSE;
            case "B":
                return Result.WIN;
            default:
                return Result.DRAW;
        }
    }

    private enum Result{
        LOSE(0){},
        DRAW(3){},
        WIN(6){};
        private final int points;
        private Result(int points){
            this.points = points;
        }
    }
}
