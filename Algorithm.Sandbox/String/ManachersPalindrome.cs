﻿using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.String
{
    /// <summary>
    /// A Manacher's longest palindrome implementation
    /// </summary>
    public class ManachersPalindrome
    {
        public int FindLongestPalindrome(string input)
        {
            if (input.Length <= 1)
            {
                throw new ArgumentException("Invalid input");
            }

            if (input.Contains("$"))
            {
                throw new Exception("Input contain sentinel character $.");
            }

            //for even length palindrome
            //we need to do this hack with $
            var array = input.ToCharArray();
            var modifiedInput = new AsStringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                modifiedInput.Append("$");
                modifiedInput.Append(array[i].ToString());
            }
            modifiedInput.Append("$");

            var result = FindLongestPalindromeR(modifiedInput.ToString());

            //remove length of $ sentinel
            return result / 2;
        }
        /// <summary>
        /// Find the longest palindrome in linear time
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int FindLongestPalindromeR(string input)
        {
            var palindromeLengths = new int[input.Length];

            int left = -1, right = 1;
            int length = 1;

            int i = 0;
            //loop through each char
            while (i < input.Length)
            {
                //terminate if end of input
                while (left >= 0 && right < input.Length)
                {
                    if (input[left] == input[right])
                    {
                        left--;
                        right++;
                        length += 2;
                    }
                    else
                    {
                        //end of current palindrome
                        break;
                    }

                }

                var @continue = false;

                //set length of current palindrome
                palindromeLengths[i] = length;

                //use mirror values on left side of palindrome
                //to fill palindrome lengths on right side of palindrome
                //so that we can save computations
                if (right > i + 2)
                {
                    var l = i - 1;
                    var r = i + 1;

                    //start from current palindrome center
                    //all the way to right end of current palindrome
                    while (r < right)
                    {
                        //find mirror char palindrome length
                        var mirrorLength = palindromeLengths[l];

                        //mirror palindrome left end exceeds
                        //current palindrom left end
                        if (l - (mirrorLength / 2) < left + 1)
                        {
                            //set length equals to maximum
                            //we can reach and then continue exploring
                            palindromeLengths[r] = (2 * (l - (left + 1))) + 1;
                            r++;
                            l--;
                            continue;
                        }
                        //mirror palindrome is totally contained
                        //in our current palindrome
                        else if ((l - (mirrorLength / 2) > left + 1
                            && r + (mirrorLength / 2) < right - 1))
                        {
                            //so just set the value and continue exploring
                            palindromeLengths[r] = palindromeLengths[l];
                            r++;
                            l--;
                            continue;
                        }
                        //mirror palindrome exactly fits inside right side
                        //of current palindrome
                        else 
                        {
                            //set length equals to maximum
                            //and then continue exploring in main loop
                            length = palindromeLengths[l];

                            //continue to main loop
                            //update state values to skip
                            //already computed values
                            i = r;
                            left = i - (length / 2) - 1;
                            right = i + (length / 2) + 1;

                            @continue = true;
                            break;

                        }

                    }

                    //already computed until i-1 by now
                    i = r;

                }

                //continue to main loop
                //states values are already set
                if (@continue)
                {
                    continue;
                }
                else
                {
                    //reset as usual
                    left = i;
                    right = i + 2;
                    length = 1;
                }

                i++;
            }

            return FindMax(palindromeLengths);
        }

        /// <summary>
        /// returns the max index in given int[] array
        /// </summary>
        /// <param name="palindromeLengths"></param>
        /// <returns></returns>
        private int FindMax(int[] palindromeLengths)
        {
            var max = int.MinValue;

            for (int i = 0; i < palindromeLengths.Length; i++)
            {
                if (max < palindromeLengths[i])
                {
                    max = palindromeLengths[i];
                }
            }

            return max;

        }
    }
}
