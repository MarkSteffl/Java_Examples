import java.io.*;
import java.util.ArrayList;
import java.util.Scanner;


public class Problem3 {

	private static File inputFile = new File ("prob3.dat");
	private static File outFile = new File ("Steffl_prob3_output.dat");
	
	public static void main (String[] args) throws FileNotFoundException {
		
		Scanner inputScanner = new Scanner ( inputFile );
		String output = "";
		
		int num_lines = inputScanner.nextInt();
		
		ArrayList<String> lines_to_decode = new ArrayList<String>();
		
		for(int i = 0; i < num_lines + 1; i++){
			lines_to_decode.add(inputScanner.nextLine());
		}
		
		for(int i = 1; i < num_lines + 1; i++){
			output += decode(lines_to_decode.get(i));
		}
		
		PrintWriter fileWriter = new PrintWriter( outFile );
		
		fileWriter.print( output );
		
		inputScanner.close();
		fileWriter.close();
	}
	
	public static String decode( String line_to_decode){
		String decoded_line = "";
		char decoded_char = ' ';
		int fromfront;
		for(int i = 0; i < line_to_decode.length(); i ++){
			decoded_char = line_to_decode.charAt(i);
			
			if((int)decoded_char >= 97 && (int)decoded_char <= 122){
				fromfront = 122 - (int)decoded_char;
				decoded_char = (char)(97 + fromfront);
			}
			if((int)decoded_char >= 65 && (int)decoded_char <= 90){
				fromfront = 90 - (int)decoded_char;
				decoded_char = (char)(65 + fromfront);
			}
			else{}
			
			decoded_line += decoded_char;
		}
		
		return decoded_line + "\r\n";
	}
	
}
