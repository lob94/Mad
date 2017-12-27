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
    
    public partial class Event
    {
        public Event()
        {
            this.Comments = new HashSet<Comment>();
            this.Recommendations = new HashSet<Recommendation>();
        }
    
        public long eventId { get; set; }
        public Nullable<long> categoryId { get; set; }
        public string name { get; set; }
        public string review { get; set; }
        public System.DateTime eventDate { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
    
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
    
    			hash = hash * multiplier + eventId.GetHashCode();
    			hash = hash * multiplier + (categoryId == null ? 0 : categoryId.GetHashCode());
    			hash = hash * multiplier + (name == null ? 0 : name.GetHashCode());
    			hash = hash * multiplier + (review == null ? 0 : review.GetHashCode());
    			hash = hash * multiplier + eventDate.GetHashCode();
    
    			return hash;
    	    }
    
    	}
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
    	public override bool Equals(object obj)
    	{
    	    Event target = (Event)obj;
    
    		return true
               &&  (this.eventId == target.eventId )       
               &&  (this.categoryId == target.categoryId )       
               &&  (this.name == target.name )       
               &&  (this.review == target.review )       
               &&  (this.eventDate == target.eventDate )       
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
    	    StringBuilder strEvent = new StringBuilder();
    
    		strEvent.Append("[ ");
           strEvent.Append(" eventId = " + eventId + " | " );       
           strEvent.Append(" categoryId = " + categoryId + " | " );       
           strEvent.Append(" name = " + name + " | " );       
           strEvent.Append(" review = " + review + " | " );       
           strEvent.Append(" eventDate = " + eventDate + " | " );       
            strEvent.Append("] ");    
    
    		return strEvent.ToString();
        }
    
    
    }
}
