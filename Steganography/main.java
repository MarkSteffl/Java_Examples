package prob1Steganography;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.PrintWriter;
import java.util.*;

public class main {
	

	public static void main(String[] args) throws FileNotFoundException
	{
		
		//create an input stream
	    Scanner inFile = new Scanner(new FileReader("prob1.dat"));
		
	    //creates an output stream
	    PrintWriter outFile = new PrintWriter("prob1output.dat");
	    
	    //variables
	    int number;
	    char decodedChar;
		
	    while (inFile.hasNextInt())
		{
	    	number = numberGet(inFile); //call incomeGet and store in yearlyIncome
	    	decodedChar = decode(number);
	    	decodedCharOutput(decodedChar, outFile);
		
		}
	    
	    inFile.close();		
	    outFile.close();
	}
	
	public static int numberGet(Scanner inFile){
		int number;  //declare input variable
		
		number=inFile.nextInt(); //records next int in file to number
		
		return number;
		}//end of numberGet
	
	public static char decode(int number){
		char decodedChar = ' ';
		int newNum;
		
		newNum = number%30;
		
		switch(newNum){
		case 0: decodedChar = ' ';
		break;
		case 1: decodedChar = ':';
		break;
		case 2: decodedChar = '?';
		break;
		case 3: decodedChar = 'A';
		break;
		case 4: decodedChar = 'B';
		break;
		case 5: decodedChar = 'C';
		break;
		case 6: decodedChar = 'D';
		break;
		case 7: decodedChar = 'E';
		break;
		case 8: decodedChar = 'F';
		break;
		case 9: decodedChar = 'G';
		break;
		case 10: decodedChar = 'H';
		break;
		case 11: decodedChar = 'I';
		break;
		case 12: decodedChar = 'J';
		break;
		case 13: decodedChar = 'K';
		break;
		case 14: decodedChar = 'L';
		break;
		case 15: decodedChar = 'M';
		break;
		case 16: decodedChar = 'N';
		break;
		case 17: decodedChar = 'O';
		break;
		case 18: decodedChar = 'P';
		break;
		case 19: decodedChar = 'Q';
		break;
		case 20: decodedChar = 'R';
		break;
		case 21: decodedChar = 'S';
		break;
		case 22: decodedChar = 'T';
		break;
		case 23: decodedChar = 'U';
		break;
		case 24: decodedChar = 'V';
		break;
		case 25: decodedChar = 'W';
		break;
		case 26: decodedChar = 'X';
		break;
		case 27: decodedChar = 'Y';
		break;
		case 28: decodedChar = 'Z';
		break;
		case 29: decodedChar = 'n';
        break;
		}
		
	
				
				return decodedChar;
		}//end of decode
	
	public static void decodedCharOutput(char decodedChar, PrintWriter oFile){
		if(decodedChar == 'n')
			oFile.println("\n");
		else
			oFile.print(decodedChar);	//outputs the decoded character into the file
		
		return;
		}//end of decodedCharOutput
	
}
