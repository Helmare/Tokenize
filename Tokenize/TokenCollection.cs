using System;
using System.Collections.Generic;

namespace Tokenize
{
    /// <summary>
    ///     A collection Tokens which includes functions to
    ///     make tokenizing and parsing easier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TokenCollection<T> : List<Token<T>> where T : struct, IConvertible
    {
        /// <summary>
        ///     The last token added.
        /// </summary>
        public Token<T> LastToken
        {
            get { return this[Count - 1]; }
        }

        /// <summary>
        ///     Adds a token to the list.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="value">The value of token.</param>
        public void Add(T type, string value)
        {
            Add(new Token<T>(type, value));
        }
        /// <summary>
        ///     Adds a token to the list.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="value">The value of token.</param>
        public void Add(T type, char value)
        {
            Add(new Token<T>(type, value.ToString()));
        }
        /// <summary>
        ///     Adds a token to the list.
        /// </summary>
        /// <param name="type">The type of token.</param>
        public void Add(T type)
        {
            Add(new Token<T>(type));
        }

        /// <summary>
        ///     Remove all the tokens with the specified type.
        /// </summary>
        /// <param name="type"></param>
        public void RemoveType(T type)
        {
            RemoveAll(token => token.Type.Equals(type));
        }

        /// <summary>
        ///     Find the index of the next Token with a specified type
        ///     after <code>index</code>.
        /// </summary>
        /// <param name="type">The type of the token that's being searched for.</param>
        /// <param name="index">The first index to search.</param>
        /// <param name="count">The number of tokens to search after the index.</param>
        /// <returns>The index of the first token with the type or -1 if none were found.</returns>
        public int IndexOfType(T type, int index, int count)
        {
            for(int i = index; i < Count || i < index + count; i++)
            {
                if (this[i].Type.Equals(type)) return i;
            }
            return -1;
        }
        /// <summary>
        ///     Find the index of the next Token with a specified type
        ///     after <code>index</code>.
        /// </summary>
        /// <param name="type">The type of the token that's being searched for.</param>
        /// <param name="index">The first index to search.</param>
        /// <returns>The index of the first token with the type or -1 if none were found.</returns>
        public int IndexOfType(T type, int index)
        {
            return IndexOfType(type, index, Count - index);
        }
        /// <summary>
        ///     Find the index of the next Token with a specified type.
        /// </summary>
        /// <param name="type">The type of the token that's being searched for.</param>
        /// <returns>The index of the first token with the type or -1 if none were found.</returns>
        public int IndexOfType(T type)
        {
            return IndexOfType(type, 0);
        }

        /// <summary>
        ///     Takes consecutive types and turns them into a single token value.
        ///     This loses all value information besides from the first in the 
        ///     group.
        /// </summary>
        /// <param name="type">The token type that is being collapsed</param>
        public void CollapseType(T type)
        {
            if (Count < 2) return; // Nothing happens if 1 or less tokens are in the list.

            // Look for tokens to remove.
            List<Token<T>> removing = new List<Token<T>>();
            for(int i = 1; i < Count; i++)
            {
                T type0 = this[i - 1].Type;
                T type1 = this[i].Type;
                if(type0.Equals(type) && type0.Equals(type1))
                {
                    removing.Add(this[i]);
                }
            }

            // Remove tokens found.
            foreach(Token<T> remove in removing)
            {
                Remove(remove);
            }
        }

        /// <summary>
        ///     Copies this TokenCollection from the starting index to the last
        ///     index (both inclusive).
        ///     
        ///     Note: If the last index is beyond the bounds of this collecton
        ///     it will only return up to the last token.
        /// </summary>
        /// <param name="start">The starting index (inclusive).</param>
        /// <param name="last">The finishing index (inclusive)</param>
        /// <returns>A TokenCollection which contains tokens from the start index to the last index.</returns>
        public TokenCollection<T> Segment(int start, int last)
        {
            TokenCollection<T> tokens = new TokenCollection<T>();
            for (int i = start; i < Count && i <= last; i++)
            {
                tokens.Add(this[i]);
            }
            return tokens;
        }

        /// <summary>
        ///     Returns an array of TokenCollections split by the
        ///     specified type.
        /// </summary>
        /// <param name="type">The type being split by.</param>
        /// <param name="includeEmpty">Whether to include the empy collections.</param>
        /// <returns>An array of TokenCollections split by the specified type.</returns>
        public TokenCollection<T>[] Split(T type, bool includeEmpty)
        {
            List<TokenCollection<T>> tokenCollections = new List<TokenCollection<T>>();
            tokenCollections.Add(new TokenCollection<T>());

            foreach(Token<T> token in this)
            {
                TokenCollection<T> last = tokenCollections[tokenCollections.Count - 1];
                if (token.Type.Equals(type))
                {
                    if (Count > 0 || includeEmpty)
                    {
                        tokenCollections.Add(new TokenCollection<T>());
                    }
                }
                else
                {
                    last.Add(token);
                }
            }

            return tokenCollections.ToArray();
        }
        /// <summary>
        ///     Returns an array of TokenCollections split by the
        ///     specified type. This includes the empty collections.
        /// </summary>
        /// <param name="type">The type being split by.</param>
        /// <param name="includeEmpty">Whether to include the empy collections.</param>
        /// <returns>An array of TokenCollections split by the specified type.</returns>
        public TokenCollection<T>[] Split(T type)
        {
            return Split(type, true);
        }

        /// <summary>
        ///     Returns whether the first token is of the specified type.
        /// </summary>
        /// <param name="type">The type the token should be.</param>
        /// <returns>Whether the first token is of the specified type.</returns>
        public bool StartsWith(T type)
        {
            return Count > 0 && this[0].Type.Equals(type);
        }
        /// <summary>
        ///     Returns whether the last token is of the specified type.
        /// </summary>
        /// <param name="type">The type the token should be.</param>
        /// <returns>Whether the first token is of the specified type.</returns>
        public bool EndsWith(T type)
        {
            return Count > 0 && LastToken.Type.Equals(type);
        }

        /// <summary>
        ///     Trim the start of the collection of all tokens
        ///     with the specified type.
        /// </summary>
        /// <param name="type"></param>
        public void TrimStart(T type)
        {
            int removeFrom = -1;
            foreach(Token<T> token in this)
            {
                if (token.Type.Equals(type)) removeFrom++;
                else break;
            }
            for(int i = removeFrom; i >= 0; i--)
            {
                RemoveAt(i);
            }
        }
        /// <summary>
        ///     Trim the end of the collection of all tokens
        ///     with the specified type.
        /// </summary>
        /// <param name="type"></param>
        public void TrimEnd(T type)
        {
            int removeFrom = Count;
            for(int i = Count - 1; i >= 0; i--)
            {
                if (this[i].Type.Equals(type)) removeFrom--;
                else break;
            }
            for(int i = removeFrom; i < Count; i++)
            {
                RemoveAt(i);
            }
        }
        /// <summary>
        ///     Trim the start and end of the collection of all tokens
        ///     with the specified type.
        /// </summary>
        /// <param name="type"></param>
        public void Trim(T type)
        {
            TrimStart(type);
            TrimEnd(type);
        }

        /// <summary>
        ///     Gives you all the tokens in Type | Lexeme format.
        /// </summary>a
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";
            foreach(Token<T> token in this)
            {
                str += token.Type.ToString() + " | " + token.Lexeme + "\n";
            }

            return str;
        }
    }
}
