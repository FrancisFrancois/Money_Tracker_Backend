using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Models
{
    // Classe JwtOptions : Contient les options de configuration pour les tokens JWT (JSON Web Tokens) utilisés dans l'application.
    public class JwtOptions
    {
        public string Issuer { get; set; } // 'Issuer' (Émetteur) : Représente l'entité qui émet le token JWT.

        public string Audience { get; set; } // 'Audience' : Spécifie le public cible du token JWT. 
                                             // Cela représente généralement l'identité attendue par le token.

        public string SigningKey { get; set; } // 'SigningKey' : Clé utilisée pour signer le token JWT.
                                               // C'est une chaîne secrète utilisée pour la signature et la vérification du token.

        public int Expiration { get; set; } // 'Expiration' : Durée de validité du token JWT, généralement exprimée en secondes.
                                            // Après cette période, le token n'est plus valide.
    }
}

