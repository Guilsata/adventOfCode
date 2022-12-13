import java.util.ArrayList;

import utilsPuzzle5.Dock;

public class Puzzle5 {
    
//                    [Q]     [P] [P]
//                [G] [V] [S] [Z] [F]
//            [W] [V] [F] [Z] [W] [Q]
//        [V] [T] [N] [J] [W] [B] [W]
//    [Z] [L] [V] [B] [C] [R] [N] [M]
//[C] [W] [R] [H] [H] [P] [T] [M] [B]
//[Q] [Q] [M] [Z] [Z] [N] [G] [G] [J]
//[B] [R] [B] [C] [D] [H] [D] [C] [N]
// 1   2   3   4   5   6   7   8   9 
// 
    public static void main(String args[]) {
        ArrayList<String> file = UtilsFile.myReadFile("data5.txt");
        game(file);
    }

    private static void game(ArrayList<String> file) {
        Dock dock = new Dock();
        for (String line : file) {
            dock.executeCommand(line);
            System.out.println(dock.ToString());
        }
        dock.printAllTopContainers();
    }
}