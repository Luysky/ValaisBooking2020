using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IBookingManager
    {
        IBookingsDB BookingDB { get; }
        Bookings AddBooking(Bookings bookings);
        List<Bookings> GetAllReservation();
        List<Bookings> GetAllReservationDate(int IdRoom, DateTime CheckIn, DateTime CheckOut);
        List<int> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut);
        void getEveryBookings(BookingManager bookingsDBManager);
        List<int> getBookingsWithRoomAndDates(BookingManager bookingsDBManager, int idRoom, DateTime checkIn, DateTime checkOut);
        void searchSimple(RoomManager roomDBManager, List<int> listRoomBooked, HotelManager hotelDBManager, PictureManager pictureDBManager, String city);
        List<Room> searchEveryAvailableRooms(RoomManager roomDBManager, HotelManager hotelDBManager, PictureManager pictureDBManager, List<int> listRoomBooked);
        void searchAdvanced(BookingManager bookingsDBManager, RoomManager roomDBManager, HotelManager hotelDBManager, PictureManager pictureDBManager, List<Object> listCriteriaRoom, List<Object> listCriteriaHotel, DateTime checkIn, DateTime checkOut);
    }
}
