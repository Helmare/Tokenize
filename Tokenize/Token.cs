using System;

namespace Tokenize
{
    /// <summary>
    ///     A type and token pair.
    /// </summary>
    /// <typeparam name="T">The enum which represents the type.</typeparam>
    public sealed class Token<T> where T : struct, IConvertible
    {
        /// <summary>
        ///     Type of token.
        /// </summary>
        public T Type { get; set; }

        /// <summary>
        ///     Value of token.
        /// </summary>
        public string Lexeme { get; set; }

        /// <summary>
        ///     Creates a token with type and value.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="lexeme">The value of the token</param>
        public Token(T type, string lexeme)
        {
            this.Type = type;
            this.Lexeme = lexeme;
        }

        /// <summary>
        ///     Creates a token with type and empty value.
        /// </summary>
        /// <param name="type">The type of token.</param>
        public Token(T type)
            : this(type, "")
        {
        }

    }
}
