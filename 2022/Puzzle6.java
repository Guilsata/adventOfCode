import java.util.ArrayList;

public class Puzzle6 {
    public static void main(String args[]) {
        ArrayList<String> file = UtilsFile.myReadFile("data6.txt");
        System.out.println(game(file));
    }

    private static int game(ArrayList<String> file){
        ArrayList<String> buffer = new ArrayList<>();
        int count = 0;
        for(String line : file){
            for(char c :line.toCharArray()){
                //4 pour part 1
                //14 pour part 2
                if(buffer.size()==14){
                    if(!(thereIsDouble(buffer))){
                        System.out.println(buffer);
                        return count;
                    }
                    else{
                        count++;
                        buffer.remove(0);
                        buffer.add(String.valueOf(c));
                    }
                }
                else{
                    count++;
                    buffer.add(String.valueOf(c));
                }
            }
        }
        return count;
    }   

    private static boolean thereIsDouble(ArrayList<String> buffer){
        for(String b :buffer){
            if(buffer.subList(buffer.indexOf(b)+1,buffer.size()).contains(b)){return true;}
        }
        return false;
    }
}
