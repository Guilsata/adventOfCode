import java.util.ArrayList;

public class Puzzle4 {
    public static void main(String args[]) {
        ArrayList<String> file = UtilsFile.myReadFile("data4.txt");
        game(file);
    }

    private static void game(ArrayList<String> file) {
        int result = 0;
        for (String line : file) {
            Couple cpl = SplitAssignment(line);
            Pair elf1 = SplitZone(cpl.left);
            Pair elf2 = SplitZone(cpl.right);
            if (elf1.overlap(elf2) || elf2.overlap(elf1)) { 
                result++;
            }
        }
        System.out.println(result);
    }

    private static Couple SplitAssignment(String assignment) {
        String[] tmp = assignment.split(",");
        return new Couple(tmp[0], tmp[1]);
    }

    private static Pair SplitZone(String cpl) {
        String[] tmp = cpl.split("-");
        return new Pair(Integer.parseInt(tmp[0]), Integer.parseInt(tmp[1]));
    }

    private record Pair(int left, int right) {
        boolean contains(Pair p) {
            if (this.left <= p.left && this.right >= p.right) {
                return true;
            } else {
                return false;
            }
        }
        boolean overlap(Pair p) {
            if ((this.left <= p.left && this.right >= p.left) || (this.left <= p.right && this.right >= p.right)) {
                return true;
            } else {
                return false;
            }
        }
    }

    private record Couple(String left, String right) {
    }
}
