package com.dalthow.healthpath.model;

import java.util.Date;

public class Appointment
{
    private int doctorId, patientId;
    private String subject, symptoms;
    private Date appointmentDate;
    private AppointmentResult appointmentResult;

    public AppointmentResult getAppointmentResult() {
        return appointmentResult;
    }

    public void setAppointmentResult(AppointmentResult appointmentResult) {
        this.appointmentResult = appointmentResult;
    }

    public int getDoctorId() {
        return doctorId;
    }

    public void setDoctorId(int doctorId) {
        this.doctorId = doctorId;
    }

    public int getPatientId() {
        return patientId;
    }

    public void setPatientId(int patientId) {
        this.patientId = patientId;
    }

    public String getSubject() {
        return subject;
    }

    public void setSubject(String subject) {
        this.subject = subject;
    }

    public String getSymptoms() {
        return symptoms;
    }

    public void setSymptoms(String symptoms) {
        this.symptoms = symptoms;
    }

    public Date getAppointmentDate() {
        return appointmentDate;
    }

    public void setAppointmentDate(Date appointmentDate) {
        this.appointmentDate = appointmentDate;
    }
}
