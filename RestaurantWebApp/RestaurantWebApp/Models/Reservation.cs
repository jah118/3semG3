using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebApp.Models
{
	public class Reservation
	{
		public int id { get; set; }
		public DateTime reservation_date { get; set; }
		public int customer_id { get; set; }
		public DateTime reservationTime { get; set; }
		public int noOfPeople { get; set; }
		public bool deposit { get; set; }
		public string note { get; set; }

		public Reservation(int id_, DateTime reservation_date_, int customer_id_, DateTime reservationTime_, int noOfPeople_, bool deposit_, string note_)
		{
			this.id = id_;
			this.reservation_date = reservation_date_;
			this.customer_id = customer_id_;
			this.reservationTime = reservationTime_;
			this.noOfPeople = noOfPeople_;
			this.deposit = deposit_;
			this.note = note_;
		}
	}
}