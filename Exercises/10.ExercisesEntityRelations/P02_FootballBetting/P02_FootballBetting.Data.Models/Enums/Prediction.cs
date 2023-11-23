namespace P02_FootballBetting.Data.Models.Enums;

//Enumerations are not entities in the DB 
//Enums are string representation of int values
//In the DB -> int
public enum Prediction
{
    Win = 1,
    Lose = 2,
    Draw = 0
}
