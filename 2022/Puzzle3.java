import java.util.ArrayList;
import java.util.Arrays;

public class Puzzle3 {
    //a = 1
    //z = 26
    //A = 27
    //Z = 52
    public static void main(String args[]){
        ArrayList<String> file = UtilsFile.myReadFile("data3.txt");
        //part1(file);
        part2(file);
    }

    private static int convertCharToInt(char cha){
        return ((int)cha)>=97?((int)cha-96):((int)cha-38);
    }

    private static void part1(ArrayList<String> file){
        int result=0;      
        char c=' ';
        for(String line : file){
            char[] tmp1 = Arrays.copyOfRange(line.toCharArray(), 0, line.length()/2);
            char[] tmp2 = Arrays.copyOfRange(line.toCharArray(), line.length()/2 , line.length());
            for(int i=0,y=0;i<line.length()/2;y++){
                if(y>=line.length()/2){i++;y=0;}
                if(tmp1[i]==tmp2[y]){
                    c=tmp1[i];
                    break;
                }
            }
            result = result + convertCharToInt(c); 
        }
        System.out.println(result);
    }

    private static void part2(ArrayList<String> file){
        int result=0;      
        char c=' ';
        ArrayList<String> tmp = new ArrayList<>();
        for(String line : file){
            if(tmp.size() == 3){
                for(char b : tmp.get(0).toCharArray()){
                    if(tmp.get(1).contains(Character.toString(b)) & tmp.get(2).contains(Character.toString(b))){
                        c=b;
                        //System.out.print(c+"-");
                        break;
                    }
                }
                tmp.clear();
                tmp.add(line);
                result = result + convertCharToInt(c); 
            }
            else {
                tmp.add(line);
            }
        }
        for(char b : tmp.get(0).toCharArray()){
            if(tmp.get(1).contains(Character.toString(b)) & tmp.get(2).contains(Character.toString(b))){
                c=b;
                System.out.print(c+"-");
                break;
            }
        }
        result = result + convertCharToInt(c); 
        System.out.println(result);
    }
}
