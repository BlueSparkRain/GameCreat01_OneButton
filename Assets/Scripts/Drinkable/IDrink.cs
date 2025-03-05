using UnityEngine;

public interface IDrink 
{
   public void DrinkMe(People people);
}

public interface IHaveEffect 
{
    public void MyEffect(People people);
}