//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.MiniPortal.Model
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    
    public partial class Recommendation
    {
        public long recommendationId { get; set; }
        public Nullable<long> userId { get; set; }
        public Nullable<long> groupId { get; set; }
        public Nullable<long> eventId { get; set; }
        public string reason { get; set; }
        public System.DateTime created { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    
    	/// <summary>
    	/// A hash code for this instance, suitable for use in hashing algorithms and data structures 
    	//  like a hash table. It uses the Josh Bloch implementation from "Effective Java"
    	/// </summary>
    	/// <returns>
    	/// Returns a hash code for this instance.
    	/// </returns>
    	public override int GetHashCode()
    	{
    	    unchecked
    	    {
    			int multiplier = 31;
    			int hash = GetType().GetHashCode();
    
    			hash = hash * multiplier + recommendationId.GetHashCode();
    			hash = hash * multiplier + (userId == null ? 0 : userId.GetHashCode());
    			hash = hash * multiplier + (groupId == null ? 0 : groupId.GetHashCode());
    			hash = hash * multiplier + (eventId == null ? 0 : eventId.GetHashCode());
    			hash = hash * multiplier + (reason == null ? 0 : reason.GetHashCode());
    			hash = hash * multiplier + created.GetHashCode();
    
    			return hash;
    	    }
    
    	}
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
    	public override bool Equals(object obj)
    	{
    	    Recommendation target = (Recommendation)obj;
    
    		return true
               &&  (this.recommendationId == target.recommendationId )       
               &&  (this.userId == target.userId )       
               &&  (this.groupId == target.groupId )       
               &&  (this.eventId == target.eventId )       
               &&  (this.reason == target.reason )       
               &&  (this.created == target.created )       
               ;
    
        }
    
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
    	public override String ToString()
    	{
    	    StringBuilder strRecommendation = new StringBuilder();
    
    		strRecommendation.Append("[ ");
           strRecommendation.Append(" recommendationId = " + recommendationId + " | " );       
           strRecommendation.Append(" userId = " + userId + " | " );       
           strRecommendation.Append(" groupId = " + groupId + " | " );       
           strRecommendation.Append(" eventId = " + eventId + " | " );       
           strRecommendation.Append(" reason = " + reason + " | " );       
           strRecommendation.Append(" created = " + created + " | " );       
            strRecommendation.Append("] ");    
    
    		return strRecommendation.ToString();
        }
    
    
    }
}
