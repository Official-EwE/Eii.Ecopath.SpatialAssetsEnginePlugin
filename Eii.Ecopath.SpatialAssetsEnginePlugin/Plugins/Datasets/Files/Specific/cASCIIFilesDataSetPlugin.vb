' SPDX-License-Identifier: EUPL-1.2
' This file is part of Ecopath with Ecosim (EwE).
' Copyright © 1991– Ecopath International Initiative (EII)'

Option Strict On
Imports System.Drawing
Imports EwECore
Imports EwECore.Common
Imports EwEUtils.Utilities


Namespace SpatialData

    ''' <summary>
    ''' JS 07/12/24: fixed number format to en-US standard (according to ChatGPT)
    ''' </summary>
    Public Class cASCIIFilesDataSetPlugin
        Inherits cMultiFileDataSetPlugin

        Private m_raster As ISpatialRaster = Nothing

        Public Sub New()
            MyBase.New()
            ' Default name and description
            Me.Name = My.Resources.DATASET_ASCII_NAME
            Me.CustomDescription = My.Resources.DATASET_ASCII_DESCRIPTION
        End Sub

#Region " Overrides "

        Public Overrides Function GetExtentAtT(dt As Date,
                                               ByRef ptfTL As System.Drawing.PointF,
                                               ByRef ptfBR As System.Drawing.PointF) As Boolean

            Dim bOK As Boolean = MyBase.GetExtentAtT(dt, ptfTL, ptfBR)

            If (bOK) Then
                ' De-spationalize
                Dim bm As cEcospaceBasemap = Me.m_core.EcospaceBasemap
                Dim dx As Single = ptfBR.X - ptfTL.X
                Dim dy As Single = ptfTL.Y - ptfBR.Y
                ptfTL = New PointF(bm.PosTopLeft.X, bm.PosTopLeft.Y)
                ptfBR = New PointF(bm.PosTopLeft.X + dx, bm.PosTopLeft.Y - dy)
            End If

            Return bOK

        End Function

        ''' -------------------------------------------------------------------
        ''' <inheritdocs cref="cFileDataSetPlugin.LoadSource"/>
        ''' -------------------------------------------------------------------
        Protected Overrides Function LoadSource() As Boolean

            Dim strFileName As String = Me.SourceFileName()
            ' Already read? Ok!
            If (Me.m_raster IsNot Nothing) Then Return True

            ' File missing?
            If (Not System.IO.File.Exists(strFileName)) Then
                ' #Yes: report error
                Me.LogMessage(cStringUtils.Localize(My.Resources.STATUS_LOAD_FILENOTFOUND, strFileName), eStatusFlags.MissingParameter)
                ' Run away
                Return False
            End If

            Try
                Dim imp As New cEcospaceImportExportASCIIData(Me.m_core)
                If (imp.Read(strFileName)) Then
                    Me.m_raster = imp.ToRaster()
                End If

            Catch ex As Exception
                ' Log generic panic message
                Me.LogMessage(cStringUtils.Localize(My.Resources.STATUS_LOAD_FAILED, ex.Message), eStatusFlags.MissingParameter)
            End Try

            ' Report all over success
            Return (Me.m_raster IsNot Nothing)

        End Function

        ''' -------------------------------------------------------------------
        ''' <inheritdocs cref="cFileDataSetPlugin.UnlockData"/>
        ''' -------------------------------------------------------------------
        Public Overrides Function UnlockData() As Boolean
            Me.m_raster = Nothing
            Return MyBase.UnlockData()
        End Function

        ''' -------------------------------------------------------------------
        ''' <inheritdocs cref="ISpatialDataSet.GetRaster"/>
        ''' -------------------------------------------------------------------
        Public Overrides Function GetRaster(converter As ISpatialDataConverter, strLayerName As String) As ISpatialRaster

            If (Not Me.IsLocked) Then Return Nothing
            Me.LoadSource()
            Return Me.m_raster

        End Function

#End Region ' Overrides

#Region " Plug-in implementation "

        ''' -----------------------------------------------------------------------
        ''' <inheritdocs cref="EwEPlugin.IPlugin.Description"/>
        ''' -----------------------------------------------------------------------
        Public Overrides ReadOnly Property Description As String
            Get
                Return "Plug-in that provides direct access to ASCII files, without requiring GDAL"
            End Get
        End Property

        ''' -----------------------------------------------------------------------
        ''' <inheritdocs cref="EwEPlugin.IPlugin.DisplayName"/>
        ''' -----------------------------------------------------------------------
        Public Overrides ReadOnly Property PluginName As String
            Get
                Return "STDF.Dataset.ASCII"
            End Get
        End Property

#End Region ' Plug-in implementation

    End Class

End Namespace
