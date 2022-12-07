import java.util.ArrayList;

public class Puzzle1 {
    public static void main(String args[]){
        ArrayList<String> file = UtilsFile.myReadFile("data1.txt");
        int lutin = 0, finalLutin, lutinCalo = 0, maxCalo = 0;
        ArrayList<Integer> tripletCalo = new ArrayList<Integer>();
        tripletCalo.add(0);
        tripletCalo.add(0);
        tripletCalo.add(0);
        for(String line : file){
            if(line.length()!=0){
                lutinCalo = lutinCalo + Integer.parseInt(line);
            }
            else{
                lutin++;
                if(isSup(lutinCalo, tripletCalo)){
                    replaceValue(lutinCalo, tripletCalo);
                }
                lutinCalo = 0;
            }
        }
        if(isSup(lutinCalo, tripletCalo)){
            replaceValue(lutinCalo, tripletCalo);
        }
        System.out.println(tripletCalo.get(0)+tripletCalo.get(1)+tripletCalo.get(2));
    }
    private static Boolean isSup(int value,ArrayList<Integer> ints){
        if(value > ints.get(0) || value > ints.get(1) || value > ints.get(2) ){return true;}
        return false;
    }   

    private static void replaceValue(int value,ArrayList<Integer> ints){
        ints.add(value);
        ints.sort(null);
        ints.remove(0);
    }
}
