using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Funhall2.Classes
{
    public class InputValidation
    {
       
        public static bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
               
                MessageBox.Show("Venligt giv dit navn");           
                 return false;
                
            }      
                    
            return true;
            
        }

        public static bool ValidateEmail(string email)
        {            
            if ((string.IsNullOrEmpty(email)))           
            {
                MessageBox.Show("enter an email"); 
                return false;
            }
            else if (!(Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")))
            {
                MessageBox.Show("Enter a valid Email"); 
                return false;
            }
            return true;                   
        }
    }
}
